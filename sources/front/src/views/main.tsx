import {useObserver} from "mobx-react";

import {Dialog, Layout} from "@focus4/layout";

import {router} from "../router";

import {Home} from "./home";
import {StarterMenu} from "./menu";
import {Profils} from "./profils";
import {Utilisateurs} from "./utilisateurs";

import "@focus4/collections/lib/focus4.collections.css";
import "@focus4/form-toolbox/lib/focus4.form-toolbox.css";
import "@focus4/forms/lib/focus4.forms.css";
import "@focus4/layout/lib/focus4.layout.css";
import "@focus4/styling/lib/focus4.styling.css";
import "@focus4/toolbox/lib/focus4.toolbox.css";
import "../main.css";

export function Main() {
    return useObserver(() => (
        <Layout menu={<StarterMenu />}>
            {(() => {
                switch (router.get()) {
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
