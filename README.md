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

- 静态数据可直接在Inspector界面进行设置，读取可通过Resources.Load<T>()方法进行读取。
- 动态数据则需要在代码中进行设置，读取可通过PlayerPrefs.GetString()方法进行读取。
