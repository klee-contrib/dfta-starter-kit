import tseslint from "typescript-eslint";

import {eslintConfig} from "@focus4/tooling";

export default tseslint.config(eslintConfig, {
    ignores: ["src/model/", "src/locale/", "src/services/", "**/*.css.d.ts"]
});
