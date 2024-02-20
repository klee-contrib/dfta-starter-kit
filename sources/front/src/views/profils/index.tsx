import {useObserver} from "mobx-react";

import {Content} from "@focus4/layout";
import {FontIcon} from "@focus4/toolbox";

import {profilStore} from "../../stores/profil";

import {router} from "../../router";
import {Header} from "../header";

import {ProfilDetail} from "./detail";
import {ProfilTable} from "./table";

export function Profils() {
    return useObserver(() => (
        <>
            <Header>
                <FontIcon>settings</FontIcon>
                <strong>
                    {!router.state.profils.proId
                        ? "Profils"
                        : `Détail du profil : ${profilStore.profil.libelle.value ?? ""}`}
                </strong>
            </Header>
            <Content>{router.state.profils.proId ? <ProfilDetail /> : <ProfilTable />}</Content>
        </>
    ));
}
