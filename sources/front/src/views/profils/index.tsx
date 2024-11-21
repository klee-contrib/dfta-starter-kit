import {useObserver} from "mobx-react";

import {Content} from "@focus4/layout";

import {profilStore} from "../../stores/profil";

import {router} from "../../router";
import {Header} from "../header";

import {ProfilDetail} from "./detail";
import {ProfilTable} from "./table";

export function Profils() {
    return useObserver(() => (
        <>
            <Header icon="settings" paramResolver={() => profilStore.profil.libelle.value ?? ""} />
            <Content>{router.state.profils.proId ? <ProfilDetail /> : <ProfilTable />}</Content>
        </>
    ));
}
