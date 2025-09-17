let jwtToken = null;
let apiServer = null;
let dotnetHelper = null;

// Handle Google login credential response
async function handleCredentialResponse(response) {
  try {
    const res = await fetch(`${apiServer}/login-google`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ idToken: response.credential }),
      credentials: "include" // ensures refresh token cookie is set
    });

    if (!res.ok) {
      const errText = await res.text();
      console.error("Login failed:", res.status, errText);
      return;
    }

    const data = await res.json();
    jwtToken = data.token; // store access token in JS memory only
    dotnetHelper.invokeMethodAsync("SetJwtToken", jwtToken); // MainLayout handles this
    console.log("Login successful, token received");
  } catch (err) {
    console.error("Error in handleCredentialResponse:", err);
  }
}

// Initialize Google login button
window.initializeGoogleLogin = async (setApiServer, setDotnetHelper) => {  
  try {
    apiServer = setApiServer;
    dotnetHelper = setDotnetHelper;
    const response = await fetch(`${apiServer}/google-client-id`);

    if (!response.ok) {
      const errText = await response.text();
      console.error("Failed to fetch google-client-id:", response.status, errText);
      return;
    }

    const data = await response.json();
    const clientId = data.clientId;

    google.accounts.id.initialize({
      client_id: clientId,
      callback: handleCredentialResponse
    });

    google.accounts.id.renderButton(
      document.querySelector(".g_id_signin"),
      { theme: "outline", size: "large" }
    );

    console.log("Google login initialized");
  } catch (err) {
    console.error("Error in initializeGoogleLogin:", err);
  }
};


// Refresh access token using HTTP-only refresh token cookie
window.refreshAccessToken = async (apiServer, dotnetHelper) => {
  try {
    const res = await fetch(`${apiServer}/refresh-token`, {
      method: "POST",
      credentials: "include"
    });

    if (res.ok) {
      const data = await res.json();
      jwtToken = data.token;
      dotnetHelper.invokeMethodAsync("SetJwtToken", jwtToken); // MainLayout handles this
      return jwtToken;
    } else {
      dotnetHelper.invokeMethodAsync("ClearJwtToken");
      return null;
    }
  } catch (err) {
    console.error("Error refreshing token:", err);
    dotnetHelper.invokeMethodAsync("ClearJwtToken");
    return null;
  }
};

window.fetchApi = async (apiServer, endpoint, method = "GET", jsonString = null) => {
  if (!jwtToken) {
    console.warn("Access token missing; call refreshAccessToken from MainLayout first");

    return JSON.stringify({
      Type: 'error',
      Status: 401,
      Message: "Missing access token"
    });
  }

  const options = {
    method,
    headers: {
      "Authorization": "Bearer " + jwtToken,
    },
    credentials: "include",
  };

  if (jsonString) {
    options.headers["Content-Type"] = "application/json";
    options.body = jsonString;
  }

  const res = await fetch(`${apiServer}${endpoint}`, options);

  const resText = await res.text();

  console.log('<DEV>', 'ok: ', res.ok, res.statusText, resText);

  if (!res.ok) {
    return JSON.stringify({
      Type: 'error',
      Status: res.status,
      Message: JSON.parse(resText)['message'] ?? ""
    });
  }

  return JSON.stringify({
    Type: 'success',
    Status: res.status,
    Data: JSON.parse(resText)
  });
};
