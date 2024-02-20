import {coreFetch} from "@focus4/core";

export async function fetch(
    method: "DELETE" | "GET" | "POST" | "PUT",
    url: string,
    {body, query}: {body?: {}; query?: {}} = {},
    options: RequestInit = {}
) {
    /* @vite-ignore */
    const {getTokenRedirect} = await import("./msal");
    const auth = await getTokenRedirect();
    if (auth) {
        options.headers = {
            Authorization: `Bearer ${auth.accessToken}`
        };
        options.credentials = "omit";

        return coreFetch(method, url, {body, query}, options);
    }
}
