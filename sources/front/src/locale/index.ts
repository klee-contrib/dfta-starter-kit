import {common} from "./common";
import {router} from "./router";
import {securite} from "./securite";

import {i18nCollections} from "@focus4/collections";
import {initI18n} from "@focus4/core";
import {i18nFormToolbox} from "@focus4/form-toolbox";
import {i18nLayout} from "@focus4/layout";
import {i18nStores} from "@focus4/stores";

initI18n("fr", [i18nCollections, i18nFormToolbox, i18nLayout, i18nStores], {
    fr: {
        common,
        securite,
        router
    }
});
