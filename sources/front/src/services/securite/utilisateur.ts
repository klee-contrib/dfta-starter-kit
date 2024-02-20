////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {fetch} from "../../server";

import {TypeUtilisateurCode} from "../../model/securite/references";
import {UtilisateurItem} from "../../model/securite/utilisateur-item";
import {UtilisateurRead} from "../../model/securite/utilisateur-read";
import {UtilisateurWrite} from "../../model/securite/utilisateur-write";

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
 * Supprime un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 */
export function deleteUtilisateur(utiId: number, options: RequestInit = {}): Promise<void> {
    return fetch("DELETE", `./api/utilisateurs/${utiId}`, {}, options);
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
 * Recherche des utilisateurs
 * @param nom Nom de l'utilisateur
 * @param prenom Nom de l'utilisateur
 * @param email Email de l'utilisateur
 * @param dateNaissance Age de l'utilisateur
 * @param adresse Adresse de l'utilisateur
 * @param actif Si l'utilisateur est actif
 * @param profilId Profil de l'utilisateur
 * @param typeUtilisateurCode Type d'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Utilisateurs matchant les critères
 */
export function searchUtilisateur(nom?: string, prenom?: string, email?: string, dateNaissance?: string, adresse?: string, actif?: boolean, profilId?: number, typeUtilisateurCode?: TypeUtilisateurCode, options: RequestInit = {}): Promise<UtilisateurItem[]> {
    return fetch("GET", `./api/utilisateurs`, {query: {nom, prenom, email, dateNaissance, adresse, actif, profilId, typeUtilisateurCode}}, options);
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
