## Unity-背包系统
### UI界面制作
整个背包界面的UI框架包括**背景，菜单栏，按钮，物品选择滚动部分，物品详细信息展示部分**

![](/Image_Markdown/背包UI界面.png)

整个背包界面的UI制作被分成五个部分，分别是**左上，上部，右上，中间，下部**

![](/Image_Markdown/界面划分.png)

其中菜单部分里的所有菜单按钮通过**Horizontal Layout Group**组件和**Content Size Fitter**组件进行布局，使得菜单按钮在界面中水平分布，并且菜单按钮的宽度是自适应的，高度是固定的。

![](/Image_Markdown/组件信息1.png)

物品选择滚动部分通过**Scroll View**组件，**Grid Layout Group**组件和**Content Size Fitter**组件进行布局，使得物品选择部分在界面中垂直均匀分布，方便物品选择。

![](/Image_Markdown/组件信息2.png)
![](/Image_Markdown/组件信息3.png)

### 存储框架设计
存储框架中包含静态数据与动态数据的存储，对于静态数据我们采用**ScriptableObject**类进行存储与设置，对于动态数据我们采用**PlayerPrefs**类将数据存储至本地磁盘。并设计GM指令来对数据的取读进行测试。

#### 静态数据可直接在**Inspector**界面进行设置，读取可通过Resources.Load<T>()方法进行读取。
1. 在代码中设计一个[**PackageTable**](https://github.com/lurenhu/Unity-Package-System/blob/main/Assets/Scripts/PackageTable.cs)类，用于存储背包中的物品数据，包括物品ID，物品名称，物品数量，物品描述，物品图片路径等。同时运用**CreateAssetMenu**特性，使得该类可以在**Inspector**界面进行创建。

2. 在**Inspector**界面中的设置如下

![](/Image_Markdown/静态数据.png)

3. 在代码中获取存储的静态数据可利用以下指令来获取

```C#
PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable_");//TableData/PackageTable_为静态数据文件夹路径
```

> 至此Unity中的静态变量便设置完毕

#### 动态数据则需要在代码中进行设置，读取可通过PlayerPrefs.GetString()方法进行读取。
1. 在代码中设计一个[**PackageLocalData**](https://github.com/lurenhu/Unity-Package-System/blob/main/Assets/Scripts/PackageLocalData.cs)类，用于存储背包系统中物品的等级，数量，以及是否为新获得物品等信息。同时在其中设置数据的存取方法。

2. 在代码中存取存储在本地的动态数据则是通过获取并添加`Item`项到 [**PackageLocalData**](https://github.com/lurenhu/Unity-Package-System/blob/main/Assets/Scripts/PackageLocalData.cs)类中，并调用`SavePackage()`方法进行存储。获取则直接调用`LoadPackage()`方法获取动态数据。

> 至此Unity中的动态变量便设置完毕

### 界面逻辑
1. 运用一个UI框架来绑定的背包界面，该UI框架包含两个类，分别是**BasePanel**和**UIManager**。**BasePanel**是所有界面的基类，**UIManager**负责管理所有的界面。

2. 创建基于**BasePanel**类的**PackagePanel**类和**PackageCell**类来绑定对应的背包主界面的各个组件以及背包中的物品显示界面的组件。其中在**PackagePanel**类中绑定背包主界面的所有按钮组件，并添加监听方法。在**PackagePanel**类中刷新滚动容器中的物品显示，在**PackageCell**类中刷新单个物品的显示。

3. 创建一个**GameManager**类和**GameManager**对象，在 **GameManager**类中封装获取静态数据和获取动态数据的方法，以及通过物品ID获取对应静态数据，通过物品UID获取对应动态数据，并对背包界面中的物品显示顺序设定规则。




