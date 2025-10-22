import {useObserver} from "mobx-react";
import {useTranslation} from "react-i18next";

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
    const {t} = useTranslation();
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
                        label: t("app.confirm.saveAndQuit"),
                        onClick: () => {
                            router.confirmation.commit(true);
                        }
                    },
                    {
                        label: t("app.confirm.quit"),
                        onClick: () => router.confirmation.commit()
                    },
                    {label: t("app.confirm.cancel"), onClick: () => router.confirmation.cancel()}
                ]}
                active={router.confirmation.pending}
                title={t("app.confirm.title")}
            >
                {t("app.confirm.text")}
            </Dialog>
        </Layout>
    ));
}
