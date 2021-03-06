using System;

namespace ModLoader.Modding
{
    public abstract class Mod : IComparable<Mod>
    {
        public int CompareTo(Mod other)
        {
            if (other == null) return 1;
            //TODO: Add dependency resolving
            return ModInfo.OfMod(this).loadingPriority.CompareTo(ModInfo.OfMod(other).loadingPriority);
        }

        /// <summary>
        ///     Called when the mod is loaded, put all main logic and registrations here
        /// </summary>
        public abstract void Load();

        /// <summary>
        ///     By default all mods will taint the game assuming it modifies some core mechanic
        /// </summary>
        /// <returns>True if the mod modifies core mechanics of the game</returns>
        public bool Taint()
        {
            return true;
        }
    }


    /// <summary>
    ///     An attribute to define all properties of your mod;
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModInfo : Attribute
    {
        /// <summary>
        ///     A simple description of what your mod does
        /// </summary>
        public string description = "";

        /// <summary>
        ///     The loading priority of your mod, the righter the value it will load before mods with a lower priority
        ///     Only change this when needed
        /// </summary>
        public int loadingPriority;

        /// <summary>
        ///     The id of your mod used by internal modloader things. It must be unique!
        /// </summary>
        public string modId;

        /// <summary>
        ///     A fancy display name for your mod
        /// </summary>
        public string name;

        /// <summary>
        ///     Your mod version, used when resolving dependencies.
        /// </summary>
        public string version;

        public ModInfo(string modId, string name, string version)
        {
            this.modId = modId;
            this.name = name;
            this.version = version;
        }

        public static ModInfo OfMod(Mod mod)
        {
            return (ModInfo) mod.GetType().GetCustomAttributes(typeof(ModInfo), true)[0];
        }
    }
}