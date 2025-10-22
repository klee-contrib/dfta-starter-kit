import {useTranslation} from "react-i18next";

import {MainMenu, MainMenuItem} from "@focus4/layout";
import {FontIcon} from "@focus4/toolbox";

import {router} from "../router";

import css from "./__style__/menu.css";

export function StarterMenu() {
    const {t} = useTranslation();
    return (
        <MainMenu activeRoute={router.get()} showOverlay theme={{list: css.list}}>
            <div className={css.logo}>
                <FontIcon>home_repair_service</FontIcon>
            </div>
            <MainMenuItem href={router.href()} icon="home" label={t("router.root")} route="" />
            <MainMenuItem
                href={router.href(x => x("utilisateurs"))}
                icon="group"
                label={t("router.utilisateurs.root")}
                route="utilisateurs"
            />
            <MainMenuItem
                href={router.href(x => x("profils"))}
                icon="settings"
                label={t("router.profils.root")}
                route="profils"
            />
        </MainMenu>
    );
}
