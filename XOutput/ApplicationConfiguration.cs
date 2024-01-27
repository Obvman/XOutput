using XOutput.Tools;

namespace XOutput
{
    public static class ApplicationConfiguration
    {
        [ResolverMethod]
        public static ArgumentParser GetArgumentParser() => new ArgumentParser();

        [ResolverMethod]
        public static HidGuardianManager GetHidGuardianManager() => new HidGuardianManager();

        [ResolverMethod]
        public static RegistryModifier GetRegistryModifier() => new RegistryModifier();

        [ResolverMethod]
        public static Devices.Input.Mouse.MouseHook GetMouseHook() => new Devices.Input.Mouse.MouseHook();
    }
}
