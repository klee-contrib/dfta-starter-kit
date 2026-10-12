import z from "zod";

import {domain, SelectCheckbox} from "@focus4/form-toolbox";

import css from "./__style__/domain.css";

export const DO_BOOLEEN = domain(z.boolean());
export const DO_CODE = domain(z.string().max(10));
export const DO_CODE_LISTE = domain(z.array(z.string().max(10)), {
    SelectComponent: SelectCheckbox<z.ZodArray<z.ZodString>>
});
export const DO_DATE = domain(z.iso.date("focus.validation.date"), {
    className: css.smallField,
    inputProps: {
        inputProps: {icon: "calendar_month"}
    }
});
export const DO_DATE_HEURE = domain(z.iso.datetime());
export const DO_EMAIL = domain(z.email("focus.validation.email"), {inputProps: {icon: "email"}});
export const DO_ENTIER = domain(z.int("focus.validation.number"));
export const DO_ID = DO_ENTIER;
export const DO_LIBELLE = domain(z.string().max(100));
export const DO_MONTANT = domain(z.number("app.validation.amount").multipleOf(0.01), {
    className: css.smallField,
    displayFormatter: {
        style: "currency",
        currency: "EUR",
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    },
    inputProps: {suffix: "€", hasThousandsSeparator: true, maxDecimals: 2, noNegativeNumbers: true}
});
