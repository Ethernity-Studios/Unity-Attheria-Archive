param($installPath, $toolsPath, $package, $project)

$file1 = $project.ProjectItems.Item("liblinux/x86/libV8_Net_Proxy.so")
$file2 = $project.ProjectItems.Item("liblinux/x64/libV8_Net_Proxy.so")
$file3 = $project.ProjectItems.Item("libosx/x86/libV8_Net_Proxy.dylib")
$file4 = $project.ProjectItems.Item("V8_Net_Proxy.dll_")
$file5 = $project.ProjectItems.Item("V8.Net.Proxy.Interface.dll.config")


# set 'Copy To Output Directory' to 'Copy if newer'
$copyToOutput1 = $file1.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 2

$copyToOutput2 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput2.Value = 2

$copyToOutput3 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput3.Value = 2

$copyToOutput4 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput4.Value = 2

$copyToOutput5 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput5.Value = 2