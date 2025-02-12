import {initColorScheme} from "@focus4/core";

import {getProfils} from "./services/securite/profil";

import {router} from "./router";
import {config, ensureSignedIn} from "./server";

initColorScheme();

fetch("./config.json").then(async res => {
    Object.assign(config, await res.json());
    await ensureSignedIn();

    await getProfils();

    await router.start();

    await import("./locale");
    await import("./views");
});
