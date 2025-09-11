let jwtToken = null;
let apiServer = null;

// Handle Google login credential response
async function handleCredentialResponse(response) {
  const res = await fetch(`${apiServer}/login-google`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ idToken: response.credential }),
    credentials: "include" // ensures refresh token cookie is set
  });

  const data = await res.json();
  jwtToken = data.token; // store access token in JS memory only
}

// Initialize Google login button
window.initializeGoogleLogin = async (setApiServer) => {
  apiServer = setApiServer;
  const response = await fetch(`${apiServer}/google-client-id`);
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

window.fetchApi = async (apiServer, endpoint) => {
  if (!jwtToken) {
    console.warn("Access token missing; call refreshAccessToken from MainLayout first");
    return "Token missing";
  }

  const res = await fetch(`${apiServer}${endpoint}`, {
    headers: { "Authorization": "Bearer " + jwtToken },
    credentials: "include"
  });

  if (!res.ok) throw new Error("Failed to fetch secure data");

  const data = await res.json();
  return JSON.stringify(data);
};