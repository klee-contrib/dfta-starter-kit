import {useObserver} from "mobx-react";
import {useState} from "react";
import {useTranslation} from "react-i18next";

import {Content, HeaderActions, Popin} from "@focus4/layout";

import {utilisateurStore} from "../../stores/utilisateur";

import {router} from "../../router";
import {Header} from "../header";

import {UtilisateurDetail} from "./detail";
import {UtilisateurList} from "./list";

export function Utilisateurs() {
    const {t} = useTranslation();

    const [utiCreation, setUtiCreation] = useState(false);
    return useObserver(() => (
        <>
            <Header
                icon="group"
                paramResolver={() =>
                    `${utilisateurStore.utilisateur.prenom.value ?? ""} ${utilisateurStore.utilisateur.nom.value ?? ""}`
                }
            >
                {!router.state.utilisateurs.utiId ? (
                    <HeaderActions
                        primary={[
                            {
                                icon: "add",
                                tooltip: {tooltip: t("app.user.add")},
                                onClick: () => setUtiCreation(true)
                            }
                        ]}
                    />
                ) : null}
            </Header>
            {router.state.utilisateurs.utiId ? (
                <Content>
                    <UtilisateurDetail />
                </Content>
            ) : (
                <>
                    <UtilisateurList />
                    <Popin
                        closePopin={() => setUtiCreation(false)}
                        opened={utiCreation}
                        preventOverlayClick={router.confirmation.active}
                    >
                        <Content>
                            <UtilisateurDetail closePopin={() => setUtiCreation(false)} />
                        </Content>
                    </Popin>
                </>
            )}
        </>
    ));
}
