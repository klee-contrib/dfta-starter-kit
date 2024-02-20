import {makeReferenceStore} from "@focus4/stores";

import * as refsPro from "../model/securite/references";
import * as refsUti from "../model/securite/references";
import { upperFirst } from "lodash";
import { fetch } from "../server";

export const referenceStore = makeReferenceStore(name => fetch("GET", `api/references/${upperFirst(name)}`), {
    droit: refsPro.droit,
    typeDroit: refsPro.typeDroit,
    typeUtilisateur: refsUti.typeUtilisateur
});
