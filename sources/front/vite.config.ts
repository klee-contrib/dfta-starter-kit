import react from "@vitejs/plugin-react";
import path from "node:path";
import {defineConfig} from "vite";
import checker from "vite-plugin-checker";

import {baseConfig, cssAutoModules} from "@focus4/tooling";

export default defineConfig({
    ...baseConfig,
    base: "",
    plugins: [cssAutoModules(/__style__/), react(), checker({typescript: true})],
    build: {
        chunkSizeWarningLimit: 1000,
        rolldownOptions: {
            input: {
                main: path.resolve(import.meta.dirname, "index.html"),
                redirect: path.resolve(import.meta.dirname, "redirect.html")
            }
        }
    },
    server: {
        port: 1357,
        proxy: {
            "/api": {target: "http://localhost:2468"}
        }
    }
});
