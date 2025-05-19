// Required namespaces
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Constants for layer sizes
        public const int SFirst = 100;   // Size of the input layer (F1)
        public const int SSecond = 4;    // Size of the category layer (F2)

        // Weight matrix from F1 to F2 (Bottom-up weights)
        public static class Weight_1__2
        {
            public static double[,] Matrix = new double[SSecond, SFirst];
        }

        // Weight matrix from F2 to F1 (Top-down weights)
        public static class Weight_2__1
        {
            public static double[,] Matrix = new double[SFirst, SSecond];
        }

        // Vigilance parameter used in ART1
        public const int Psie = 2;
        public static double RoGreek;

        public Form1()
        {
            InitializeComponent();
        }

        // Converts a 2D matrix to a 1D vector
        public double[] Convert_Matrix_to_Vector(double[,] a)
        {
            int index = 0;
            double[] b = new double[a.GetLength(0) * a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    b[index++] = a[i, j];
                }
            }

            return b;
        }

        // Converts a 1D vector to a 2D matrix with specified dimensions
        public double[,] Convert_Vector_to_Matrix(double[] a, int Row, int Col)
        {
            double[,] b = new double[Row, Col];
            int index = 0;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (index < a.Length)
                        b[i, j] = a[index++];
                }
            }

            return b;
        }

        // Display a 1D vector in label1
        public void Write_Vector(double[] a)
        {
            label1.Text = "My Vector is:\n";
            for (int i = 0; i < a.Length; i++)
            {
                label1.Text += " & " + a[i];
            }
        }

        // Display a 2D matrix in label3
        public void Write_Matrix(double[,] a)
        {
            label3.Text = "My Matrix is:\n";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    label3.Text += " & " + a[i, j];
                }
                label3.Text += "\n";
            }
        }

        // Display a memory matrix in label1 (used for showing weights or stored patterns)
        public void Write_Matrix_Of_Memory(double[,] a)
        {
            label1.Text = "Memory Matrix is:\n";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    label1.Text += " & " + a[i, j];
                }
                label1.Text += "\n";
            }
        }

        // Generate a decreasing vector and display its matrix form
        public void Read_Vector()
        {
            double[] c = new double[9];
            int value = 40;

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = value--;
            }

            double[,] matrix = Convert_Vector_to_Matrix(c, 3, 3);
            Write_Vector(c);
            Write_Matrix(matrix);
        }

        // Generate an increasing matrix and display its vector form
        public void Read_Matrix()
        {
            double[,] matrix = new double[3, 3];
            int value = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = value * 2;
                    value++;
                }
            }

            Write_Matrix(matrix);
            double[] vector = Convert_Matrix_to_Vector(matrix);
            Write_Vector(vector);
        }

        // Triggered when the form loads
        private void Form1_Load(object sender, EventArgs e)
        {
            // You can call Read_Vector or Read_Matrix here to show test data on load
            // Read_Vector();
            // Read_Matrix();
        }
    }

        
        // Event handlers for paint events - currently empty but can be used for custom drawing if needed
private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
{
    // Custom paint logic for tableLayoutPanel1 can be added here
}
private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
{
    // Custom cell paint logic can be added here
}
private void tableLayoutPanel1_DoubleClick(object sender, EventArgs e)
{
    // Logic for double click event on tableLayoutPanel1
}

// Mouse click event handler for table layout panels.
// Calls Draw_Button to add or remove a button based on click position.
private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
{
    Draw_Button(e, tableLayoutPanel1);
}
private void tableLayoutPanel2_MouseClick(object sender, MouseEventArgs e)
{
    Draw_Button(e, tableLayoutPanel2);
}
private void tableLayoutPanel3_MouseClick(object sender, MouseEventArgs e)
{
    Draw_Button(e, tableLayoutPanel3);
}
private void tableLayoutPanel4_MouseClick(object sender, MouseEventArgs e)
{
    Draw_Button(e, tableLayoutPanel4);
}

/// <summary>
/// Adds or removes a Button control in the clicked cell of the TableLayoutPanel.
/// If the clicked cell is empty, it adds a button; if a button exists, it removes it.
/// </summary>
/// <param name="e">Mouse event args containing click location</param>
/// <param name="table">The TableLayoutPanel being clicked</param>
public void Draw_Button(MouseEventArgs e, TableLayoutPanel table)
{
    int row = 0;
    int verticalOffset = 0;

    // Iterate over all rows
    foreach (int rowHeight in table.GetRowHeights())
    {
        int column = 0;
        int horizontalOffset = 0;

        // Iterate over all columns in the current row
        foreach (int colWidth in table.GetColumnWidths())
        {
            // Define the cell rectangle to check if the click falls inside it
            Rectangle cellRect = new Rectangle(horizontalOffset, verticalOffset, colWidth, rowHeight);

            // If click is inside this cell
            if (cellRect.Contains(e.Location))
            {
                Control existingControl = table.GetControlFromPosition(column, row);

                if (existingControl == null)
                {
                    // Add new button if cell is empty
                    Button btn = new Button
                    {
                        Width = colWidth - 13,  // Adjust size for padding
                        Height = rowHeight - 13
                    };
                    table.Controls.Add(btn, column, row);
                }
                else
                {
                    // Remove button if already exists
                    table.Controls.Remove(existingControl);
                }

                return; // Exit after handling the clicked cell
            }
            horizontalOffset += colWidth;
            column++;
        }
        verticalOffset += rowHeight;
        row++;
    }
}

/// <summary>
/// Returns a 2D matrix representing the presence (1) or absence (0) of buttons in the TableLayoutPanel cells.
/// </summary>
public double[,] Get_table_Matrix(TableLayoutPanel table)
{
    double[,] matrix = new double[table.RowCount, table.ColumnCount];

    for (int i = 0; i < table.RowCount; i++)
    {
        for (int j = 0; j < table.ColumnCount; j++)
        {
            Control c = table.GetControlFromPosition(j, i);
            matrix[i, j] = (c == null) ? 0 : 1;
        }
    }

    return matrix;
}

/// <summary>
/// Sets buttons in the TableLayoutPanel according to the input matrix (1 = add button, 0 = remove button)
/// </summary>
public void Set_Matrix_For_Table(double[,] matrix, TableLayoutPanel table)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Control c = table.GetControlFromPosition(j, i);

            if (matrix[i, j] == 0)
            {
                // Remove button if matrix cell is zero and button exists
                if (c != null) table.Controls.Remove(c);
            }
            else if (matrix[i, j] == 1 && c == null)
            {
                // Add button if matrix cell is one and button does not exist
                Button btn = new Button
                {
                    Width = 10,
                    Height = 10
                };
                table.Controls.Add(btn, j, i);
            }
        }
    }
}

// Handlers for "Present" buttons - pass respective TableLayoutPanel to training method
private void Present_Click(object sender, EventArgs e) => Take_Pattern_And_Train(tableLayoutPanel1);
private void Present2_Click(object sender, EventArgs e) => Take_Pattern_And_Train(tableLayoutPanel2);
private void Present3_Click(object sender, EventArgs e) => Take_Pattern_And_Train(tableLayoutPanel3);
private void Present4_Click(object sender, EventArgs e) => Take_Pattern_And_Train(tableLayoutPanel4);

/// <summary>
/// Extracts pattern matrix from TableLayoutPanel, converts to vector, and starts training.
/// </summary>
public void Take_Pattern_And_Train(TableLayoutPanel table)
{
    double[,] matrix = Get_table_Matrix(table);
    double[] vector = Convert_Matrix_to_Vector(matrix); // You need to implement or have this method

    label1.Text = "Ro is :" + (RoGreek / 10);

    ART1_Training_for_Patern(vector);
}

/// <summary>
/// Initialize weight matrices with default values.
/// </summary>
public void Initialize()
{
    for (int i = 0; i < Weight_2__1.Matrix.GetLength(0); i++)
        for (int j = 0; j < Weight_2__1.Matrix.GetLength(1); j++)
            Weight_2__1.Matrix[i, j] = 1;

    double z = 0.0198;

    for (int i = 0; i < Weight_1__2.Matrix.GetLength(0); i++)
        for (int j = 0; j < Weight_1__2.Matrix.GetLength(1); j++)
            Weight_1__2.Matrix[i, j] = z;
}

/// <summary>
/// Multiplies a matrix by a vector and returns the resulting vector.
/// </summary>
public double[] Multiply_Matrix_by_Vector(double[,] matrix, double[] vector)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    if (cols != vector.Length)
    {
        MessageBox.Show("Dimensions of Matrix and vector are not equal");
        return null;
    }

    double[] result = new double[rows];

    for (int i = 0; i < rows; i++)
    {
        result[i] = 0;
        for (int j = 0; j < cols; j++)
            result[i] += matrix[i, j] * vector[j];
    }

    return result;
}

/// <summary>
/// Extracts a row vector from a matrix.
/// </summary>
public double[] Take_Vector_From_Matrix_By_Row(double[,] matrix, int rowNumber)
{
    int cols = matrix.GetLength(1);
    double[] vector = new double[cols];

    for (int j = 0; j < cols; j++)
        vector[j] = matrix[rowNumber, j];

    return vector;
}

/// <summary>
/// Extracts a column vector from a matrix.
/// </summary>
public double[] Take_Vector_From_Matrix_By_Column(double[,] matrix, int columnNumber)
{
    int rows = matrix.GetLength(0);
    double[] vector = new double[rows];

    for (int i = 0; i < rows; i++)
        vector[i] = matrix[i, columnNumber];

    return vector;
}

/// <summary>
/// Sets a row of a matrix from a vector.
/// </summary>
public double[,] Set_Vector_For_Matrix_By_Row(double[,] matrix, double[] vector, int rowNumber)
{
    if (matrix.GetLength(1) != vector.Length)
    {
        MessageBox.Show("Dimensions of Matrix and vector are not equal");
        return null;
    }

    for (int j = 0; j < vector.Length; j++)
        matrix[rowNumber, j] = vector[j];

    return matrix;
}

/// <summary>
/// Sets a column of a matrix from a vector.
/// </summary>
public double[,] Set_Vector_For_Matrix_By_Column(double[,] matrix, double[] vector, int columnNumber)
{
    if (matrix.GetLength(0) != vector.Length)
    {
        MessageBox.Show("Dimensions of Matrix and vector are not equal");
        return null;
    }

    for (int i = 0; i < vector.Length; i++)
        matrix[i, columnNumber] = vector[i];

    return matrix;
}

/// <summary>
/// Calculates the norm (sum of elements) of a vector.
/// </summary>
public double Calculate_Norm_Of_Vector(double[] vector)
{
    double sum = 0;
    for (int i = 0; i < vector.Length; i++)
        sum += vector[i];
    return sum;
}

/// <summary>
/// Calculates element-wise intersection (logical AND) of two vectors.
/// Returns vector with 1 where both vectors have 1, else 0.
/// </summary>
public double[] Calculate_Intersect_Of_Vectors(double[] vector1, double[] vector2)
{
    if (vector1.Length != vector2.Length)
    {
        MessageBox.Show("Dimensions of vectors are not equal");
        return null;
    }

    double[] result = new double[vector1.Length];
    for (int i = 0; i < vector1.Length; i++)
    {
        result[i] = (vector1[i] == 1 && vector2[i] == 1) ? 1 : 0;
    }
    return result;
}

// Event handler for trackBar2 scroll event
private void trackBar2_Scroll(object sender, EventArgs e)
{
    // Update the RoGreek variable to the current value of the trackbar
    RoGreek = trackBar2.Value;

    // Update the UI labels to reflect the new value
    TrackBarLabel.Text = "Value is : " + RoGreek.ToString();
    label3.Text = "Value is : " + RoGreek.ToString();
}
        
    }

        
}

