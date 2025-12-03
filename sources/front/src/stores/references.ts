import {upperFirst} from "es-toolkit";

import {makeReferenceStore} from "@focus4/stores";

import * as refs from "../model/securite/references";

import fetch from "../server";

export const referenceStore = makeReferenceStore(name => fetch(`api/references/${upperFirst(name)}`).json(), {
    droit: refs.droit,
    typeDroit: refs.typeDroit,
    typeUtilisateur: refs.typeUtilisateur
});
