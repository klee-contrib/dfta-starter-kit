////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {fetch} from "../../server";

import {TypeUtilisateurCode} from "../../model/securite/references";
import {UtilisateurItem} from "../../model/securite/utilisateur/utilisateur-item";
import {UtilisateurRead} from "../../model/securite/utilisateur/utilisateur-read";
import {UtilisateurWrite} from "../../model/securite/utilisateur/utilisateur-write";

/**
 * Ajoute un utilisateur
 * @param utilisateur Utilisateur à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Utilisateur sauvegardé
 */
export function addUtilisateur(utilisateur: UtilisateurWrite, options: RequestInit = {}): Promise<UtilisateurRead> {
    return fetch("POST", `./api/utilisateurs`, {body: utilisateur}, options);
}

/**
 * Ajoute une nouvelle photo pour un utilisateur.
 * @param utiId Id de l'utilisateur
 * @param photo Photo.
 * @param options Options pour 'fetch'.
 */
export function addUtilisateurPhoto(utiId: number, photo: File, options: RequestInit = {}): Promise<void> {
    const body = new FormData();
    fillFormData(
        {
            photo
        },
        body
    );
    return fetch("PUT", `./api/utilisateurs/${utiId}/photo`, {body}, options);
}

/**
 * Supprime un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 */
export function deleteUtilisateur(utiId: number, options: RequestInit = {}): Promise<void> {
    return fetch("DELETE", `./api/utilisateurs/${utiId}`, {}, options);
}

/**
 * Supprime la photo d'un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 */
export function deleteUtilisateurPhoto(utiId: number, options: RequestInit = {}): Promise<void> {
    return fetch("DELETE", `./api/utilisateurs/${utiId}/photo`, {}, options);
}

/**
 * Charge le détail d'un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Le détail de l'utilisateur
 */
export function getUtilisateur(utiId: number, options: RequestInit = {}): Promise<UtilisateurRead> {
    return fetch("GET", `./api/utilisateurs/${utiId}`, {}, options);
}

/**
 * Charge la photo d'un utilisateur (si elle existe).
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Photo.
 */
export function getUtilisateurPhoto(utiId: number, options: RequestInit = {}): Promise<Response> {
    return fetch("GET", `./api/utilisateurs/${utiId}/photo`, {}, options);
}

/**
 * Recherche des utilisateurs
 * @param nom Nom de l'utilisateur
 * @param prenom Nom de l'utilisateur
 * @param email Email de l'utilisateur
 * @param dateNaissance Age de l'utilisateur
 * @param actif Si l'utilisateur est actif
 * @param profilId Profil de l'utilisateur
 * @param typeUtilisateurCode Type d'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Utilisateurs matchant les critères
 */
export function searchUtilisateur(nom?: string, prenom?: string, email?: string, dateNaissance?: string, actif?: boolean, profilId?: number, typeUtilisateurCode?: TypeUtilisateurCode, options: RequestInit = {}): Promise<UtilisateurItem[]> {
    return fetch("GET", `./api/utilisateurs`, {query: {nom, prenom, email, dateNaissance, actif, profilId, typeUtilisateurCode}}, options);
}

/**
 * Sauvegarde un utilisateur
 * @param utiId Id de l'utilisateur
 * @param utilisateur Utilisateur à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Utilisateur sauvegardé
 */
export function updateUtilisateur(utiId: number, utilisateur: UtilisateurWrite, options: RequestInit = {}): Promise<UtilisateurRead> {
    return fetch("PUT", `./api/utilisateurs/${utiId}`, {body: utilisateur}, options);
}

function fillFormData(data: any, formData: FormData, prefix = "") {
    if (Array.isArray(data)) {
        data.forEach((item, i) => fillFormData(item, formData, prefix + (typeof item === "object" && !(item instanceof File) ? `[${i}]` : "")));
    } else if (typeof data === "object" && !(data instanceof File)) {
        for (const key in data) {
            fillFormData(data[key], formData, (prefix ? `${prefix}.` : "") + key);
        }
    } else {
        formData.append(prefix, data);
    }
}
