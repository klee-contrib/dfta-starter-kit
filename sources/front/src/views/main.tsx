import {useObserver} from "mobx-react";

import {Dialog, Layout} from "@focus4/layout";

import {router} from "../router";

import {Home} from "./home";
import {StarterMenu} from "./menu";
import {Profils} from "./profils";
import {Utilisateurs} from "./utilisateurs";

import "../main.css";

export function Main() {
    return useObserver(() => (
        <Layout menu={<StarterMenu />}>
            {(() => {
                switch (router.get(a => a)) {
                    case "utilisateurs":
                        return <Utilisateurs />;
                    case "profils":
                        return <Profils />;
                    default:
                        return <Home />;
                }
            })()}
            <Dialog
                actions={[
                    {
                        color: "primary",
                        label: "Sauvegarder et quitter",
                        onClick: () => {
                            router.confirmation.commit(true);
                        }
                    },
                    {
                        label: "Quitter sans sauvegarder",
                        onClick: () => router.confirmation.commit()
                    },
                    {label: "Annuler", onClick: () => router.confirmation.cancel()}
                ]}
                active={router.confirmation.pending}
                title="Confirmation"
            >
                Êtes-vous sûr de vouloir quitter la page ?
            </Dialog>
        </Layout>
    ));
}
