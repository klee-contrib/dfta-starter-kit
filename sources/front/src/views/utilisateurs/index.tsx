import {useObserver} from "mobx-react";

import {Content} from "@focus4/layout";
import {FontIcon} from "@focus4/toolbox";

import {utilisateurStore} from "../../stores/utilisateur";

import {router} from "../../router";
import {Header} from "../header";

import {UtilisateurDetail} from "./detail";
import {UtilisateurList} from "./list";

export function Utilisateurs() {
    return useObserver(() => (
        <>
            <Header>
                <FontIcon>group</FontIcon>
                <strong>
                    {!router.state.utilisateurs.utiId
                        ? "Utilisateurs"
                        : `Détail de l'utilisateur : ${utilisateurStore.utilisateur.prenom.value ?? ""} ${
                              utilisateurStore.utilisateur.nom.value ?? ""
                          }`}
                </strong>
            </Header>
            {router.state.utilisateurs.utiId ? (
                <Content>
                    {" "}
                    <UtilisateurDetail />
                </Content>
            ) : (
                <UtilisateurList />
            )}
        </>
    ));
}
