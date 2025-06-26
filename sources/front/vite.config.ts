import react from "@vitejs/plugin-react";
import {defineConfig} from "vite";
import checker from "vite-plugin-checker";
import oxlintPlugin from "vite-plugin-oxlint";

import {baseConfig, cssAutoModules} from "@focus4/tooling";

export default defineConfig({
    ...baseConfig,
    base: "",
    plugins: [cssAutoModules(/__style__/), react(), checker({typescript: true}), oxlintPlugin()],
    server: {
        port: 1357,
        proxy: {
            "/api": {target: "http://localhost:2468"}
        }
    }
});
