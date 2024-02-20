import {makeRouter, param} from "@focus4/core";

export const router = makeRouter({
    profils: param("proId", a => a.number()),
    utilisateurs: param("utiId", a => a.number())
});
