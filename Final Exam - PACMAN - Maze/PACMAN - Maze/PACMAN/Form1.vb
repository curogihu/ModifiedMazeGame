Public Class Form1
    Const PICTURE_FOLDER As String = "C:\Users\IT-DEP1\Documents\Visual Studio 2013\Projects\finalExam\PacmanChars"
    Const MAX_ROW As Integer = 10
    Const MAX_COL As Integer = 10

    Dim PicBox(MAX_ROW, MAX_COL) As PictureBox
    Dim MazeBox(MAX_ROW, MAX_COL) As Integer        ' 0 = accessible, 1 = not accessible
    Dim Placeholder As New PictureBox
    Dim pictop As Integer
    Dim picleft As Integer
    Dim TimerCounter As Integer
    Dim Direction As String
    Dim CurrentRow As Integer
    Dim CurrentCol As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize() ' to set random wall

        Call displaygrid()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TimerCounter = TimerCounter + 1
        If TimerCounter = 100 Then TimerCounter = 0
        If TimerCounter Mod 2 = 0 Then
            If Direction = "Right" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\Closedright.jpg")
            End If
            If Direction = "Left" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\ClosedLeft.jpg")
            End If
            If Direction = "Down" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\ClosedDown.jpg")
            End If
            If Direction = "Up" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\ClosedUp.jpg")
            End If
        Else
            If Direction = "Right" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\openright.jpg")
            End If
            If Direction = "Left" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\OpenLeft.jpg")
            End If
            If Direction = "Down" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\OpenDown.jpg")
            End If
            If Direction = "Up" Then
                PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\OpenUp.jpg")
            End If

        End If
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Up Then
            Direction = "Up"

            CurrentRow = CurrentRow - 1
            'you are at row one and  press up key 
            If CurrentRow = -1 Then
                Beep()
                CurrentRow += 1
                Return

            ElseIf MazeBox(CurrentRow, CurrentCol) = 1 Then
                Beep()
                CurrentRow += 1
                Return

            End If
            PicBox(0, 0).Top = PicBox(0, 0).Top - 100
            PicBox(CurrentRow, CurrentCol).Image = Image.FromFile(PICTURE_FOLDER + "\Empty.jpg")
        End If

        If e.KeyCode = Keys.Down Then
            Direction = "Down"
            'start moving 
            CurrentRow = CurrentRow + 1

            If CurrentRow = MAX_ROW Then
                Beep()
                CurrentRow -= 1
                Return

            ElseIf MazeBox(CurrentRow, CurrentCol) = 1 Then
                Beep()
                CurrentRow -= 1
                Return

            End If

            PicBox(0, 0).Top = PicBox(0, 0).Top + 100
            PicBox(CurrentRow, CurrentCol).Image = Image.FromFile(PICTURE_FOLDER + "\Empty.jpg")
        End If

        If e.KeyCode = Keys.Left Then
            Direction = "Left"
            'start moving 
            CurrentCol = CurrentCol - 1

            If CurrentCol = -1 Then
                Beep()
                CurrentCol += 1
                Return

            ElseIf MazeBox(CurrentRow, CurrentCol) = 1 Then
                Beep()
                CurrentCol += 1
                Return

            End If

            PicBox(0, 0).Left = PicBox(0, 0).Left - 100
            PicBox(CurrentRow, CurrentCol).Image = Image.FromFile(PICTURE_FOLDER + "\Empty.jpg")
        End If

        If e.KeyCode = Keys.Right Then
            Direction = "Right"
            'start moving 
            CurrentCol = CurrentCol + 1

            If CurrentCol = MAX_COL Then
                Beep()
                CurrentCol -= 1
                Return

            ElseIf MazeBox(CurrentRow, CurrentCol) = 1 Then
                Beep()
                CurrentCol -= 1
                Return

            End If

            PicBox(0, 0).Left = PicBox(0, 0).Left + 100
            PicBox(CurrentRow, CurrentCol).Image = Image.FromFile(PICTURE_FOLDER + "\Empty.jpg")
        End If

        If e.KeyCode = Keys.G Then
            MessageBox.Show("You gave up this game.")
            Me.Close()
        End If

        If CurrentRow = MAX_ROW - 1 And CurrentCol = MAX_COL - 1 Then
            MessageBox.Show("Game clear!! Congratulation!!")
            Me.Close()
        End If

    End Sub

    Public Sub displaygrid()

        Dim wallIdx As Integer

        'making the form autosize so it resizes itself automatically 
        Me.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.AutoSize = True
        'initial location of the pacman character 
        CurrentRow = 0
        CurrentCol = 0
        'This is the first picturebox underneath pacman character 
        Placeholder.Top = 0
        Placeholder.Left = 0
        Placeholder.Height = 100
        Placeholder.Width = 100
        Placeholder.Image = Image.FromFile(PICTURE_FOLDER + "\Empty.jpg")
        Controls.Add(Placeholder)

        For i = 0 To MAX_ROW - 1
            wallIdx = GetValidWallIndex(i, CInt(Rnd() * MAX_COL))

            For j = 0 To MAX_COL - 1
                PicBox(i, j) = New PictureBox
                PicBox(i, j).Height = 100
                PicBox(i, j).Width = 100
                PicBox(i, j).Top = 100 * i
                PicBox(i, j).Left = 100 * j
                'PicBox(i, j).Image = Image.FromFile(PICTURE_FOLDER + "\dot.jpg")

                If j = wallIdx Then
                    'Without setting image before adding, it may occur exception.
                    PicBox(i, j).Image = Image.FromFile(PICTURE_FOLDER + "\wall.jpg")
                End If

                Controls.Add(PicBox(i, j))
                MazeBox(i, j) = 0

            Next

            MazeBox(i, wallIdx) = 1
        Next

        PicBox(0, 0).BringToFront()
        PicBox(0, 0).Image = Image.FromFile(PICTURE_FOLDER + "\closedright.jpg")
        Direction = "Right"
        Timer1.Start()
        Timer1.Interval = 500

    End Sub

    Function GetValidWallIndex(pRow As Integer, pWallIndex As Integer) As Integer
        Dim tmpWallIndex = pWallIndex

        Select Case pRow

            Case 0
                If tmpWallIndex = 0 Then
                    tmpWallIndex += 1
                End If

            Case MAX_ROW - 1
                If tmpWallIndex = MAX_COL - 1 Then
                    tmpWallIndex -= 1
                End If


        End Select

        Return tmpWallIndex

    End Function
End Class
