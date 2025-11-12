!include MUI2.nsh
Name "AIActions"
!define APPNAME "AIActions"
!define INSTALLDIR "$PROGRAMFILES64\${APPNAME}"
!define SOURCEPATH "..\bin\Publish\net8.0-windows"
!define MUI_ICON "${SOURCEPATH}\data\icons\context_menu_icon.ico"
!insertmacro MUI_LANGUAGE "English"
LangString MUI_TEXT_DIRECTORY_TITLE ${LANG_ENGLISH} ${APPNAME}
LangString MUI_TEXT_DIRECTORY_SUBTITLE ${LANG_ENGLISH} "Choose an installation directory"


OutFile "AIActions_Setup.exe"
InstallDir "${INSTALLDIR}"
ShowInstDetails show
ShowUninstDetails show
RequestExecutionLevel admin

; Pages
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH



; Installer
Section "Install ${APPNAME}" SecMain
  !insertmacro MUI_HEADER_TEXT ${APPNAME} "Installation"
  SetOutPath "$INSTDIR"

  DetailPrint "Removing old installation files..."
  RMDir /r "$INSTDIR\data"
  Delete "$INSTDIR\AIActions.exe"
  Delete "$INSTDIR\AIActions.pbd"

  DetailPrint "Copying new files..."
  File /r "${SOURCEPATH}\*"

  WriteUninstaller "$INSTDIR\unins.exe"
  
  
  DetailPrint "Changing folder permissions..."
  ExecWait 'icacls "$INSTDIR\data\python" /grant Everyone:(M) /T'

  DetailPrint "Registering to context menu..."
  ExecWait '"$INSTDIR\AIActions.exe" register'

  DetailPrint "Installation complete!"
SectionEnd


; Uninstaller
Section "Uninstall"
  !insertmacro MUI_HEADER_TEXT ${APPNAME} "Uninstalling"
  DetailPrint "Removing files..."
  RMDir /r "$INSTDIR"
  RMDir /r "$AppData\AIActions"

  DetailPrint "Removing context menu registry keys..."

  DeleteRegKey HKCR "Directory\background\shell\AIActions"
  DeleteRegKey HKCR "*\shell\AIActions"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"

  DetailPrint "Uninstallation complete!"
SectionEnd


; Add to Windows uninstall menu.
Section -Post
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\Uninstall.exe"
SectionEnd
