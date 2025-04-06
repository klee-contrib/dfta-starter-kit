import i18next from "i18next";
import {initReactI18next} from "react-i18next";

import {i18nCollections} from "@focus4/collections";
import {baseI18nextConfig} from "@focus4/core";
import {i18nFormToolbox} from "@focus4/form-toolbox";
import {i18nLayout} from "@focus4/layout";
import {i18nStores} from "@focus4/stores";

import {common} from "./common";
import {router} from "./router";
import {securite} from "./securite";

i18next.use(initReactI18next).init({
    ...baseI18nextConfig([i18nCollections, i18nFormToolbox, i18nLayout, i18nStores], {
        fr: {
            common,
            securite,
            router
        }
    }),
    lng: "fr"
});
