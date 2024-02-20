import {CSSElement, CSSMod} from "@focus4/styling";

interface Icon { _4f9b6: void }
interface Line { _d4ad5: void }
interface Text { _fe864: void }

export interface LineCss {
    icon: CSSElement<Icon>;
    line: CSSElement<Line>;
    "line--profil": CSSMod<"profil", Line>;
    text: CSSElement<Text>;
    "text--sub": CSSMod<"sub", Text>;
    "text--title": CSSMod<"title", Text>;
}

declare const lineCss: LineCss;
export default lineCss;
