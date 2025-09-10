let jwtToken = null;

function handleCredentialResponse(response, dotnetHelper) {
    console.log("Encoded JWT ID token: " + response.credential);

    fetch("http://localhost:5123/login-google", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ idToken: response.credential })
    })
    .then(res => res.json())
    .then(data => {
        console.log("JWT from API:", data.token);
        jwtToken = data.token;
        dotnetHelper.invokeMethodAsync("SetJwtToken", jwtToken);
    })
    .catch(err => console.error(err));
}

window.initializeGoogleLogin = (dotnetHelper) => {
    google.accounts.id.initialize({
        client_id: "892014516730-6qql563r3ms5ae90sri27pi1pjcci4kr.apps.googleusercontent.com",
        callback: response => handleCredentialResponse(response, dotnetHelper)
    });

    google.accounts.id.renderButton(
        document.querySelector(".g_id_signin"),
        { theme: "outline", size: "large" }
    );
};

window.fetchSecureData = async (token) => {
    try {
        const res = await fetch("http://localhost:5123/me", {
            headers: { "Authorization": "Bearer " + token }
        });
        const data = await res.json();
        return JSON.stringify(data, null, 2);
    } catch (err) {
        console.error(err);
        return "Error fetching secure data";
    }
};
