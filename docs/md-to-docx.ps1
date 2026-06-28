# md-to-docx.ps1 - Convert a Markdown file to a valid .docx (OpenXML, no Word required).
# Usage: powershell -ExecutionPolicy Bypass -File md-to-docx.ps1 -InputMd <in.md> -OutputDocx <out.docx>
param(
    [Parameter(Mandatory = $true)] [string] $InputMd,
    [Parameter(Mandatory = $true)] [string] $OutputDocx
)

$ErrorActionPreference = 'Stop'

function Esc([string]$s) {
    if ($null -eq $s) { return "" }
    # drop illegal XML control chars (keep tab=9)
    $sb = New-Object System.Text.StringBuilder
    foreach ($ch in $s.ToCharArray()) {
        $code = [int]$ch
        if ($code -ge 32 -or $code -eq 9) { [void]$sb.Append($ch) }
    }
    $t = $sb.ToString()
    $t = $t -replace '&', '&amp;'
    $t = $t -replace '<', '&lt;'
    $t = $t -replace '>', '&gt;'
    $t = $t -replace '"', '&quot;'
    return $t
}

function Clean-Inline([string]$t) {
    if ($null -eq $t) { return "" }
    $t = [regex]::Replace($t, '\[([^\]]+)\]\([^\)]*\)', '$1')  # [text](url)->text
    $t = $t -replace '\*\*', ''
    $t = $t -replace '`', ''
    return $t.Trim()
}
function Is-TableRow([string]$line) { $l = $line.Trim(); return ($l.StartsWith('|') -and $l.EndsWith('|') -and $l.Length -gt 1) }
function Is-TableSep([string]$line) { return ($line.Trim() -match '^\|[\s\:\-\|]+\|$') }
function Split-Row([string]$line) {
    $l = $line.Trim(); $l = $l.Substring(1, $l.Length - 2)
    return ($l -split '\|') | ForEach-Object { Clean-Inline($_) }
}

function Para([string]$text, [string]$style) {
    $p = '<w:p>'
    if ($style) { $p += '<w:pPr><w:pStyle w:val="' + $style + '"/></w:pPr>' }
    $p += '<w:r><w:t xml:space="preserve">' + (Esc $text) + '</w:t></w:r></w:p>'
    return $p
}
function ParaBullet([string]$text) {
    return '<w:p><w:pPr><w:ind w:left="360" w:hanging="360"/></w:pPr><w:r><w:t xml:space="preserve">' + (Esc ("- " + $text)) + '</w:t></w:r></w:p>'
}
function ParaQuote([string]$text) {
    return '<w:p><w:pPr><w:ind w:left="360"/><w:rPr><w:i/><w:color w:val="595959"/></w:rPr></w:pPr><w:r><w:rPr><w:i/><w:color w:val="595959"/></w:rPr><w:t xml:space="preserve">' + (Esc $text) + '</w:t></w:r></w:p>'
}
function ParaCode([string]$text) {
    return '<w:p><w:pPr><w:rPr><w:rFonts w:ascii="Consolas" w:hAnsi="Consolas"/><w:sz w:val="18"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii="Consolas" w:hAnsi="Consolas"/><w:sz w:val="18"/></w:rPr><w:t xml:space="preserve">' + (Esc $text) + '</w:t></w:r></w:p>'
}
function CellPara([string]$text, [bool]$bold) {
    $rpr = ''
    if ($bold) { $rpr = '<w:rPr><w:b/></w:rPr>' }
    return '<w:p>' + $rpr.Replace('<w:rPr>', '<w:pPr><w:rPr>').Replace('</w:rPr>', '</w:rPr></w:pPr>') + '<w:r>' + $rpr + '<w:t xml:space="preserve">' + (Esc $text) + '</w:t></w:r></w:p>'
}

$lines = Get-Content -LiteralPath $InputMd -Encoding UTF8
$body = New-Object System.Text.StringBuilder
$inCode = $false
$i = 0
while ($i -lt $lines.Count) {
    $line = $lines[$i].TrimEnd()

    if ($line.Trim().StartsWith('```')) { $inCode = -not $inCode; $i++; continue }
    if ($inCode) { [void]$body.Append((ParaCode $lines[$i])); $i++; continue }
    if ($line.Trim().Length -eq 0) { $i++; continue }
    if ($line.Trim() -match '^---+$') { $i++; continue }

    if (Is-TableRow $line) {
        $block = @()
        while ($i -lt $lines.Count -and (Is-TableRow $lines[$i].TrimEnd())) {
            if (-not (Is-TableSep $lines[$i].TrimEnd())) { $block += , (Split-Row $lines[$i].TrimEnd()) }
            $i++
        }
        if ($block.Count -gt 0) {
            $cols = ($block | ForEach-Object { $_.Count } | Measure-Object -Maximum).Maximum
            $colW = [math]::Floor(9026 / $cols)
            $tbl = '<w:tbl><w:tblPr><w:tblW w:w="5000" w:type="pct"/>'
            $tbl += '<w:tblBorders>'
            foreach ($b in 'top', 'left', 'bottom', 'right', 'insideH', 'insideV') {
                $tbl += '<w:' + $b + ' w:val="single" w:sz="4" w:space="0" w:color="A6A6A6"/>'
            }
            $tbl += '</w:tblBorders><w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/></w:tblPr>'
            $tbl += '<w:tblGrid>'
            for ($c = 0; $c -lt $cols; $c++) { $tbl += '<w:gridCol w:w="' + $colW + '"/>' }
            $tbl += '</w:tblGrid>'
            for ($r = 0; $r -lt $block.Count; $r++) {
                $isHeader = ($r -eq 0)
                $tbl += '<w:tr>'
                for ($c = 0; $c -lt $cols; $c++) {
                    $val = ''
                    if ($c -lt $block[$r].Count) { $val = $block[$r][$c] }
                    $tcPr = '<w:tcPr><w:tcW w:w="' + $colW + '" w:type="dxa"/>'
                    if ($isHeader) { $tcPr += '<w:shd w:val="clear" w:color="auto" w:fill="D9D9D9"/>' }
                    $tcPr += '</w:tcPr>'
                    $tbl += '<w:tc>' + $tcPr + (CellPara $val $isHeader) + '</w:tc>'
                }
                $tbl += '</w:tr>'
            }
            $tbl += '</w:tbl><w:p/>'
            [void]$body.Append($tbl)
        }
        continue
    }

    if ($line -match '^#\s+(.*)')   { [void]$body.Append((Para (Clean-Inline $Matches[1]) 'Title'));    $i++; continue }
    if ($line -match '^##\s+(.*)')  { [void]$body.Append((Para (Clean-Inline $Matches[1]) 'Heading1')); $i++; continue }
    if ($line -match '^###\s+(.*)') { [void]$body.Append((Para (Clean-Inline $Matches[1]) 'Heading2')); $i++; continue }
    if ($line -match '^####\s+(.*)'){ [void]$body.Append((Para (Clean-Inline $Matches[1]) 'Heading3')); $i++; continue }
    if ($line -match '^\s*[-\*]\s+(.*)')  { [void]$body.Append((ParaBullet (Clean-Inline $Matches[1]))); $i++; continue }
    if ($line -match '^\s*(\d+\.)\s+(.*)'){ [void]$body.Append((Para (Clean-Inline ($Matches[1] + ' ' + $Matches[2])) '')); $i++; continue }
    if ($line -match '^>\s?(.*)')   { [void]$body.Append((ParaQuote (Clean-Inline $Matches[1]))); $i++; continue }

    [void]$body.Append((Para (Clean-Inline $line) ''))
    $i++
}

$sectPr = '<w:sectPr><w:pgSz w:w="11906" w:h="16838"/><w:pgMar w:top="1134" w:right="1134" w:bottom="1134" w:left="1134" w:header="708" w:footer="708" w:gutter="0"/></w:sectPr>'
$documentXml = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' +
'<w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">' +
'<w:body>' + $body.ToString() + $sectPr + '</w:body></w:document>'

$contentTypes = @'
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/><Override PartName="/word/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml"/></Types>
'@

$rels = @'
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/></Relationships>
'@

$docRels = @'
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/></Relationships>
'@

$stylesXml = @'
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<w:styles xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main"><w:docDefaults><w:rPrDefault><w:rPr><w:rFonts w:ascii="Calibri" w:hAnsi="Calibri" w:cs="Calibri"/><w:sz w:val="22"/><w:szCs w:val="22"/><w:lang w:val="vi-VN"/></w:rPr></w:rPrDefault><w:pPrDefault><w:pPr><w:spacing w:after="120" w:line="276" w:lineRule="auto"/></w:pPr></w:pPrDefault></w:docDefaults><w:style w:type="paragraph" w:default="1" w:styleId="Normal"><w:name w:val="Normal"/></w:style><w:style w:type="paragraph" w:styleId="Title"><w:name w:val="Title"/><w:basedOn w:val="Normal"/><w:next w:val="Normal"/><w:pPr><w:spacing w:before="240" w:after="240"/></w:pPr><w:rPr><w:b/><w:color w:val="1F3864"/><w:sz w:val="56"/><w:szCs w:val="56"/></w:rPr></w:style><w:style w:type="paragraph" w:styleId="Heading1"><w:name w:val="heading 1"/><w:basedOn w:val="Normal"/><w:next w:val="Normal"/><w:pPr><w:keepNext/><w:spacing w:before="280" w:after="120"/><w:outlineLvl w:val="0"/></w:pPr><w:rPr><w:b/><w:color w:val="2E74B5"/><w:sz w:val="32"/><w:szCs w:val="32"/></w:rPr></w:style><w:style w:type="paragraph" w:styleId="Heading2"><w:name w:val="heading 2"/><w:basedOn w:val="Normal"/><w:next w:val="Normal"/><w:pPr><w:keepNext/><w:spacing w:before="200" w:after="80"/><w:outlineLvl w:val="1"/></w:pPr><w:rPr><w:b/><w:color w:val="2E74B5"/><w:sz w:val="26"/><w:szCs w:val="26"/></w:rPr></w:style><w:style w:type="paragraph" w:styleId="Heading3"><w:name w:val="heading 3"/><w:basedOn w:val="Normal"/><w:next w:val="Normal"/><w:pPr><w:keepNext/><w:spacing w:before="160" w:after="80"/><w:outlineLvl w:val="2"/></w:pPr><w:rPr><w:b/><w:color w:val="1F4E79"/><w:sz w:val="24"/><w:szCs w:val="24"/></w:rPr></w:style></w:styles>
'@

# Build the .docx (zip) with UTF-8 (no BOM) entries
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem
$out = [System.IO.Path]::GetFullPath($OutputDocx)
if (Test-Path -LiteralPath $out) { Remove-Item -LiteralPath $out -Force }
$enc = New-Object System.Text.UTF8Encoding($false)
$fs = [System.IO.File]::Open($out, [System.IO.FileMode]::Create)
$zip = New-Object System.IO.Compression.ZipArchive($fs, [System.IO.Compression.ZipArchiveMode]::Create)

function Add-Entry($zip, $path, $content, $enc) {
    $e = $zip.CreateEntry($path, [System.IO.Compression.CompressionLevel]::Optimal)
    $s = $e.Open()
    $bytes = $enc.GetBytes($content)
    $s.Write($bytes, 0, $bytes.Length)
    $s.Dispose()
}

Add-Entry $zip '[Content_Types].xml' $contentTypes $enc
Add-Entry $zip '_rels/.rels' $rels $enc
Add-Entry $zip 'word/document.xml' $documentXml $enc
Add-Entry $zip 'word/_rels/document.xml.rels' $docRels $enc
Add-Entry $zip 'word/styles.xml' $stylesXml $enc

$zip.Dispose()
$fs.Close()
Write-Output ("DOCX created (OpenXML): " + $out)
