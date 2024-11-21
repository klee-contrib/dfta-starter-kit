import {useObserver} from "mobx-react";

import {Content} from "@focus4/layout";

import {utilisateurStore} from "../../stores/utilisateur";

import {router} from "../../router";
import {Header} from "../header";

import {UtilisateurDetail} from "./detail";
import {UtilisateurList} from "./list";

export function Utilisateurs() {
    return useObserver(() => (
        <>
            <Header
                icon="group"
                paramResolver={() =>
                    `${utilisateurStore.utilisateur.prenom.value ?? ""} ${utilisateurStore.utilisateur.nom.value ?? ""}`
                }
            />
            {router.state.utilisateurs.utiId ? (
                <Content>
                    <UtilisateurDetail />
                </Content>
            ) : (
                <UtilisateurList />
            )}
        </>
    ));
}
