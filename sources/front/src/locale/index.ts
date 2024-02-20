import {translation as focus} from "focus4";
import i18next from "i18next";
import numeral from "numeral";

import "numeral/locales/fr";

import {common} from "./common";
import {securite} from "./securite";

i18next.init({
    lng: "fr",
    resources: {fr: {translation: {focus, common, securite}}},
    nsSeparator: "🤷‍♂️"
});
numeral.locale("fr");
