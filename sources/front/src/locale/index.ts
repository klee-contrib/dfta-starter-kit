import i18next from "i18next";
import {initReactI18next} from "react-i18next";

import {i18nCollections} from "@focus4/collections";
import {baseI18nextConfig, coreConfig} from "@focus4/core";
import {i18nFormToolbox} from "@focus4/form-toolbox";
import {i18nLayout} from "@focus4/layout";
import {i18nStores} from "@focus4/stores";

import {en} from "./en-US";
import {fr} from "./fr-FR";

coreConfig.useI18nextAcceptHeader = true;
i18next.use(initReactI18next).init({
    ...baseI18nextConfig([i18nCollections, i18nFormToolbox, i18nLayout, i18nStores], {
        "fr-FR": fr,
        "en-US": en
    }),
    lng: localStorage.getItem("lng") ?? "fr-FR",
    fallbackLng: "fr-FR"
});
