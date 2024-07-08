add-type -AssemblyName System.Windows.Forms

add-Type -TypeDefinition @"
    using System;
    using System.Runtime.InteropServices;

    public class WinAPI {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }
"@

function SetWindowPosition {
    param(
        [System.Diagnostics.Process] $target, 
        [int]$x,
        [int]$y, 
        [int]$width,
        [int]$height
    )

    ## Get the target window handle
    ## $target = (Get-Process | Where-Object { $_.Id -eq $processId } | Select-Object -First 1);
    $hwnd = $target.MainWindowHandle;
    $count = 5; 
    while($hwnd -eq 0){
        Start-Sleep -Seconds 1
        $hwnd = $target.MainWindowHandle;
        $count = $count - 1;
        if($count -eq 0){
            Write-Host "Failed to get window handle"
            return 0;
        }
    }
    # Write-Host $hwnd
    ## Get the current screen resolution
    $screen = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds;
    ## Resize and move the window
    
    ## get the with of the screen
    $w = $screen.Width / $width
    $left = $x * $w;
    $right = $left + $w;
    $h = $screen.Height / $width; 
    $top = $y * $h ;
    $bottom = $top + $h;

    ## Write-Host $w $left $right $h $top $bottom

    $res = [WinAPI]::MoveWindow($hwnd, $left, $top, $w, $h, $true);

    if (-not $res) {
        Write-Host "Failed to move window: $res"
    }

}