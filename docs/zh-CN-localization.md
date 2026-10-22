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
