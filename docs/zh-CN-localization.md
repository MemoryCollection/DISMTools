# 简体中文界面本地化（zh-CN Localization）

DISMTools 现已支持简体中文界面。本说明记录本地化的实现方式与使用方法。

## 如何启用中文

1. 打开 **选项（Options）** 对话框。
2. 在 **Language（语言）** 下拉框中选择 **Chinese（中文）**。
3. 应用后，主窗体及各个功能面板的界面文字将切换为中文。

若选择 **Use system language（使用系统语言）** 且当前系统区域为中文（zh-CN / zh-Hans / zh-Hant），程序也会自动应用中文界面。

## 实现方式

DISMTools 的界面文本由 `MainForm.ChangeLangs(LangCode)` 集中驱动，并在各面板中以
`Select Case LangCode / Language` 代码块硬编码（原支持 en/es/fr/pt/it）。

本次本地化新增了语言代码 **6 = 简体中文（zh-CN）**：

- `MainForm.vb`：在 `ChangeLangs` 中新增 `Case 6`，并为系统语言为中文时增加自动映射到 6 的重映射；
  语言上限由 `If Language > 5` 放宽至 `If Language > 6`。
- 各面板语言块（`ProgressPanel.vb`、`AddPackage.vb`、`AppInstallerDownloader.vb`、
  `DriverManualFilePicker.vb`、`NewUnattendWiz.vb` 等）均新增 `Case 6`。
- `Options.Designer.vb`：语言下拉框新增 `Chinese` 选项（索引 6）。
- `Utilities/LanguageUtilities/LanguageHelper.vb`：新增 `CHS` / `CHT` → 6 的映射。
- `MainForm.vb` 的 Tour 链接新增 `languageCode = "zh"`。

翻译统一采用 DISM 领域术语：image=映像、mount=挂载、commit=提交、package=程序包、
feature=功能、capability=功能包、driver=驱动程序、provisioning=预配、unattended=无人参与。

## 说明

- 自动模式（系统中文）下主窗体会显示中文；个别面板在自动分支未做 CHS 重映射时可能仍显示英文，
  可在各面板语言例程顶部仿照 `ChangeLangs` 增加 `CHS → 6` 重映射以补全。
- 仓库根目录 `lang_work/` 下保留翻译字典（`translations.json`）与抽取/注入脚本，便于后续维护与扩展其他语言。
- 构建（CI）会编译本地化后的代码，验证 `Case 6` 注入未破坏原有结构。

## CI 构建依赖的修复

原 `nugetpkgprep.bat` 依赖仓库内的 `pkgsrc.zip`（LFS 大文件）来填充 `packages/` 目录，
但本地化提交中该文件被排除，导致 CI 中 `packages/` 为空、`CheckMissingDlls.ps1` 复制
`System.Net.Http` 等 DLL 时失败。已将 CI 改为在 `create-nightly-installer.yaml` 中通过
`nuget restore packages.config` 直接从 NuGet 还原依赖，构建不再依赖 LFS 压缩包，可复现且更标准。

> 构建诊断：若 `Generate installer` 步骤失败，工作流会把 `CheckMissingDlls.ps1` 的异常与
> `msbuild` 的错误行以 `::error::` 注解输出，便于定位是依赖缺失还是 VB 编译错误。
>
> `BDELib.dll` 是 `Tools/BDELib` 工程的产物（被 `DISMTools.vbproj` 以 ProjectReference 引用）。
> CI 现会在编译主工程前先显式构建 `BDELib` 并复制到 `bin/Debug`，修复 `BC2017 找不到 BDELib.dll`。

## 译文编码约束（重要）

VB 编译器（Roslyn）把 **U+201C/U+201D（全角引号）也视为字符串定界符**。因此中译文里不能出现
`“ ”`，否则字符串会被提前截断，报 `BC30205 End of statement expected`。
本项目统一改用 `「 」`。同理，ASCII 双引号在 VB 字符串中必须写成 `""` 转义。

> 其他修复：`DriverManualFilePicker.vb` 的多行字符串（`& CrLf & _` 续行）曾被注入脚本截断，
> 导致 `BC30201`；已改为完整的两行续行写法。

## 构建结果

`Create Nightly Installer` 的 `build-runspace` 作业已在 GitHub Actions 上**编译成功**，
证明中文 `Case 6` 注入未破坏 VB 语法结构。

`upload-installer` 作业已改为**发布到本仓库**：使用仓库自带的 `GITHUB_TOKEN`
（`permissions: contents: write`）创建/覆盖 `nightly-<分支名>` 的 GitHub Release 并附上
`dt_setup.exe`，不再依赖外部仓库 `dt-nightly-installers` 与 `SSH_DEPLOY_KEY`/`API_TOKEN_GITHUB`。
若仓库的 Actions 工作流权限为只读，需在 Settings → Actions → General → Workflow permissions
中改为 "Read and write permissions"。
