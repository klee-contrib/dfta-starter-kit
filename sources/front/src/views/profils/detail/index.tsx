import {ScrollspyContainer} from "@focus4/layout";

import {ProfilInfos} from "./informations";
import {ProfilUtilisateurs} from "./utilisateurs";

export function ProfilDetail() {
    return (
        <ScrollspyContainer>
            <ProfilInfos />
            <ProfilUtilisateurs />
        </ScrollspyContainer>
    );
}
