////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Clients.Db.Reference;

/// <summary>
/// This interface was automatically generated. It contains all the operations to load the reference lists declared in module Securite.
/// </summary>
[RegisterContract]
public partial interface ISecuriteReferenceAccessors
{
    /// <summary>
    /// Reference accessor for type Droit.
    /// </summary>
    /// <returns>List of Droit.</returns>
    [ReferenceAccessor]
    ICollection<Droit> LoadDroits();

    /// <summary>
    /// Reference accessor for type TypeDroit.
    /// </summary>
    /// <returns>List of TypeDroit.</returns>
    [ReferenceAccessor]
    ICollection<TypeDroit> LoadTypeDroits();

    /// <summary>
    /// Reference accessor for type TypeUtilisateur.
    /// </summary>
    /// <returns>List of TypeUtilisateur.</returns>
    [ReferenceAccessor]
    ICollection<TypeUtilisateur> LoadTypeUtilisateurs();
}
