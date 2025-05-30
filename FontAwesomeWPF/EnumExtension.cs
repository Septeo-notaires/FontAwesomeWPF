using System;
using System.ComponentModel;
using System.Linq;

namespace FontAwesomeWPF
{
    public static class EnumExtension
    {
        /// <summary>
        /// Recupere la valeur (int) de l'enum
        /// </summary>
        /// <param name="value">Methode d'extension d'une enumération</param>
        public static int ToInt(this Enum value) => Convert.ToInt32(value);

        /// <summary>
        /// Methode d'extension permettant de récupérer la valeur de l'attribut "Description"
        /// e.g. [Description("Day of week. Sunday")]
        /// </summary>
        /// <param name="value">Methode d'extension d'une enumération</param>
        /// <returns>Valeur de l'attribut Description</returns>
        public static string ToDescription(this Enum value) => value.GetAttributeValue<DescriptionAttribute, string>(attr => attr.Description);

        /// <summary>

        /// <summary>
        /// Cette methode d'extension permet de récupèrer l'attribut d'une énumération
        /// </summary>
        /// <typeparam name="T">Type de l'attribut</typeparam>
        /// <returns>Attribut concerné</returns>
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            return memberInfo?.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        /// <summary>
        /// Cette methode d'extension permet de récupèrer la valeur d'un attribut
        /// </summary>
        /// <typeparam name="TAttribute">Type de l'attribut</typeparam>
        /// <typeparam name="TResult">Type du resultat attendu</typeparam>
        /// <param name="selector">Selecteur de la propriete de l'attribut a recuperer</param>
        /// <returns>Valeur de l'attribut a recuperer</returns>
        private static TResult GetAttributeValue<TAttribute, TResult>(this Enum value, Func<TAttribute, TResult> selector) where TAttribute : Attribute
        {
            var attribute = value.GetAttribute<TAttribute>();
            return attribute != null ? selector(attribute) : default;
        }
    }

    public enum EnumAttributeFrom
    {
        Name,
        DisplayName,
        ShortName,
        Int,
        Code
    }
}