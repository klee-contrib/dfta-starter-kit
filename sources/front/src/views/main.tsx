import {useObserver} from "mobx-react";

import {Layout} from "@focus4/layout";

import {router} from "../router";

import {Home} from "./home";
import {StarterMenu} from "./menu";
import {Profils} from "./profils";
import {Utilisateurs} from "./utilisateurs";

import "./__style__/main.css";

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
        </Layout>
    ));
}
