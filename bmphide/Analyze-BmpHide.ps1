function Analyze-BmpHide
{
    [CmdletBinding()] Param(
    [Parameter(Mandatory = $true)]
    [String]$FunctionName,
    [Parameter(Mandatory = $true)]
    [String]$AssemblyPath
    )
    Clear-Host
    $AssemblyFullPath = Convert-Path -Path $AssemblyPath
    $BmpHideAssembly = [System.Reflection.Assembly]::LoadFile($AssemblyFullPath)
    $Types = $BmpHideAssembly.GetTypes()
    $Program = $Types | where Name -eq "Program"
    Init-Variables($Program)
    switch ($FunctionName)
    {
        { $_ -eq "a" }{ Invoke-A($Program) }
        { $_ -eq "b" }{ Invoke-B($Program) }
        { $_ -eq "c" }{ Invoke-C($Program) }
        { $_ -eq "d" }{ Invoke-D($Program) }
        { $_ -eq "e" }{ Invoke-E($Program) }
        { $_ -eq "f" }{ Invoke-F($Program) }
        { $_ -eq "g" }{ Invoke-G($Program) }
        { $_ -eq "j" }{ Invoke-J($Program) }
        default { Write-Output "[!] Invalid Function Name" }
    }
}

function Init-Variables{
    Param([System.Reflection.TypeInfo]$Program)
    $fields = $Program.DeclaredFields
    $yy = 20
    $yy *= 136
    $yy += 18
    $fields[0].SetValue($null, $yy)
    $ww = "1F7D"
    $ww += "1482"
    $fields[1].SetValue($null, $ww)
    $zz = "MzQxOTk="
    $fields[2].SetValue($null, $zz)
}

function Convert-ToBinary
{
    Param([Int64]$Number, [Int32]$Width)
    $BinaryString = [System.Convert]::ToString($Number, 2)
    while ($BinaryString.Length -lt $Width)
    {
        $BinaryString = '0' + $BinaryString
    }
    return $BinaryString
}

function Invoke-A{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("a")
    foreach ($b in 0 .. 127)
    {
        foreach ($r in 0 .. 127)
        {
            $args = [Byte]$b, [Byte]$r
            $out = $Method.Invoke($null, $args)
            Write-Output ([System.String]::Format("[{0}:B] -> b: {1}, r: {2}, out: {3}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $args[1] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
            Write-Output ([System.String]::Format("[{0}:D] -> b: {1}, r: {2}, out: {3}", $Method.Name, $args[0], $args[1], $out))
            Write-Output "********************************"
        }
        Write-Output "================================================================="
    }
}

function Invoke-B{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("b")
    foreach ($b in 0 .. 255)
    {
        $args = [Byte]$b, [Byte]7
        $out = $Method.Invoke($null, $args)
        Write-Output ([System.String]::Format("[{0}:B] -> b: {1}, r: {2}, out: {3}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $args[1] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
        Write-Output ([System.String]::Format("[{0}:D] -> b: {1}, r: {2}, out: {3}", $Method.Name, $args[0], $args[1], $out))
        Write-Output "********************************"
    }
    Write-Output "================================================================="
}

function Invoke-C{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("c")
    foreach ($b in 0 .. 127)
    {
        foreach ($r in 0 .. 127)
        {
            $args = [Byte]$b, [Byte]$r
            $out = $Method.Invoke($null, $args)
            Write-Output ([System.String]::Format("[{0}:B] -> b: {1}, r: {2}, out: {3}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $args[1] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
            Write-Output ([System.String]::Format("[{0}:D] -> b: {1}, r: {2}, out: {3}", $Method.Name, $args[0], $args[1], $out))
            Write-Output "********************************"
        }
        Write-Output "================================================================="
    }
}

function Invoke-D{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("d")
    foreach ($b in 0 .. 255)
    {
        $args = [Byte]$b, [Byte]3
        $out = $Method.Invoke($null, $args)
        Write-Output ([System.String]::Format("[{0}:B] -> b: {1}, r: {2}, out: {3}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $args[1] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
        Write-Output ([System.String]::Format("[{0}:D] -> b: {1}, r: {2}, out: {3}", $Method.Name, $args[0], $args[1], $out))
        Write-Output "********************************"
    }
    Write-Output "================================================================="
}

function Invoke-E{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("e")
    foreach ($b in 0 .. 255)
    {
        foreach ($r in 0 .. 255)
        {
            $args = [Byte]$b, [Byte]$r
            $out = $Method.Invoke($null, $args)
            Write-Output ([System.String]::Format("[{0}:B] -> b: {1}, r: {2}, out: {3}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $args[1] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
            Write-Output ([System.String]::Format("[{0}:D] -> b: {1}, r: {2}, out: {3}", $Method.Name, $args[0], $args[1], $out))
            Write-Output "********************************"
        }
        Write-Output "================================================================="
    }
}

function Invoke-F{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("f")
    foreach ($idx in 0 .. 255)
    {
        $args = $idx
        $out = $Method.Invoke($null, $args)
        Write-Output ([System.String]::Format("[{0}:B] -> idx: {1}, out: {2}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
        Write-Output ([System.String]::Format("[{0}:D] -> idx: {1:X}, out: {2:X}", $Method.Name, $args[0], $out))
        Write-Output "********************************"
    }
    Write-Output "================================================================="
}

function Invoke-G{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("g")
    foreach ($idx in 0 .. 255)
    {
        $args = $idx
        $out = $Method.Invoke($null, $args)
        Write-Output ([System.String]::Format("[{0}:B] -> idx: {1}, out: {2}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
        Write-Output ([System.String]::Format("[{0}:D] -> idx: {1}, out: {2}", $Method.Name, $args[0], $out))
        Write-Output "********************************"
    }
    Write-Output "================================================================="
}


function Invoke-J{
    Param([System.Reflection.TypeInfo]$Program)
    $Method = $Program.GetMethod("j")
    foreach ($z in 0 .. 255)
    {
        $args = [Byte]$z
        $out = $Method.Invoke($null, $args)
        Write-Output ([System.String]::Format("[{0}:B] -> z: {1}, out: {2}", $Method.Name, (Convert-ToBinary -Number $args[0] -Width 8), (Convert-ToBinary -Number $out -Width 8)))
        Write-Output ([System.String]::Format("[{0}:D] -> z: {1}, out: {2}", $Method.Name, $args[0], $out))
        Write-Output "********************************"
    }
    Write-Output "================================================================="
}