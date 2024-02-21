import {useObserver} from "mobx-react";
import {ReactNode} from "react";

import {HeaderItem, HeaderScrolling, HeaderTopRow} from "@focus4/layout";
import {colorScheme, toBem} from "@focus4/styling";
import {IconButton, Switch} from "@focus4/toolbox";

import {signOut, userStore} from "../server";

import css from "./__style__/header.css";

const theme = toBem(css);

export function Header({children}: {children?: ReactNode}) {
    return useObserver(() => (
        <HeaderScrolling canDeploy={false} theme={{scrolling: theme.header()}}>
            <HeaderTopRow>
                <HeaderItem theme={{item: theme.item()}}>
                    <strong>Starter Kit Focus v4</strong>
                </HeaderItem>
                <HeaderItem fillWidth theme={{item: theme.item()}}>
                    {children}
                </HeaderItem>
                <HeaderItem theme={{item: theme.item({user: true})}}>
                    <strong>{userStore.login}</strong>
                    <IconButton className={css.button} icon="account_circle" onClick={signOut} />
                    <Switch
                        iconOff="light_mode"
                        iconOn="dark_mode"
                        onChange={() => (colorScheme.dark = !colorScheme.dark)}
                        value={colorScheme.dark}
                    />
                </HeaderItem>
            </HeaderTopRow>
        </HeaderScrolling>
    ));
}
