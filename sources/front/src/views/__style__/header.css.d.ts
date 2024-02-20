import {CSSElement, CSSMod} from "@focus4/styling";

interface Button { _c7660: void }
interface Header { _c1afb: void }
interface Item { _e862a: void }

export interface HeaderCss {
    button: CSSElement<Button>;
    header: CSSElement<Header>;
    item: CSSElement<Item>;
    "item--user": CSSMod<"user", Item>;
}

declare const headerCss: HeaderCss;
export default headerCss;
