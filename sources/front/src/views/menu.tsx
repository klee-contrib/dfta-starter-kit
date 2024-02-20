import {MainMenu, MainMenuItem} from "@focus4/layout";
import {FontIcon} from "@focus4/toolbox";

import {router} from "../router";

import css from "./__style__/menu.css";

export function StarterMenu() {
    return (
        <MainMenu activeRoute={router.get(x => x)} showOverlay>
            <div className={css.logo}>
                <FontIcon>home_repair_service</FontIcon>
            </div>
            <MainMenuItem icon="home" label="Accueil" onClick={() => router.to(x => x)} route="" />
            <MainMenuItem
                icon="group"
                label="Utilisateurs"
                onClick={() => router.to(x => x("utilisateurs"))}
                route="utilisateurs"
            />
            <MainMenuItem
                icon="settings"
                label="Profils"
                onClick={() => router.to(x => x("profils"))}
                route="profils"
            />
        </MainMenu>
    );
}
