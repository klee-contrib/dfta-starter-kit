import {coreFetch} from "@focus4/core";

import {getTokenRedirect} from "./msal";

export {config, ensureSignedIn, signOut, userStore} from "./msal";

export default coreFetch.extend({
    hooks: {
        beforeRequest: [
            async request => {
                const auth = await getTokenRedirect();
                if (auth) {
                    request.headers.set("Authorization", `Bearer ${auth.accessToken}`);
                }
            }
        ]
    }
});
