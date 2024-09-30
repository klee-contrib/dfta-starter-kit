import {upperFirst} from "lodash";

import {makeReferenceStore} from "@focus4/stores";

import * as refs from "../model/securite/references";

import {fetch} from "../server";

export const referenceStore = makeReferenceStore(name => fetch("GET", `api/references/${upperFirst(name)}`), {
    droit: refs.droit,
    typeDroit: refs.typeDroit,
    typeUtilisateur: refs.typeUtilisateur
});
