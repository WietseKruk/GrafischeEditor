package sample;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Group;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.shape.Rectangle;
import javafx.stage.Stage;
import javafx.scene.control.TextField;
import javafx.fxml.FXML;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.event.Event;
import javafx.scene.Node;
import javafx.collections.ObservableList;
import javafx.scene.paint.Color;
import javafx.animation.FadeTransition;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.scene.*;
import javafx.application.Application;
import javafx.scene.shape.Line;
import javafx.stage.Stage;
import javafx.event.*;
import java.lang.*;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.shape.Rectangle;

public class Controller extends Application {

    @FXML public TextField xfield;
    @FXML public TextField yfield;
    @FXML public TextField widthfield;
    @FXML public TextField heightfield;

    private int x, y, width, height;
    ObservableList<Node> rootChildren;

    @Override
    public void start(Stage primaryStage) throws Exception{
        //Parent root = FXMLLoader.load(getClass().getResource("GraphicalEditor.fxml"));

        final Group root = new Group();
        rootChildren = root.getChildren();

        final Scene scene = new Scene(root, 1620, 670);
        scene.setCursor(Cursor.HAND);

        Button drawRectangle = new Button("Rectangle");
        rootChildren.add(drawRectangle);

        drawRectangle.setOnAction(actionEvent -> {
//            x = Integer.parseInt(xfield.getText());
//            y = Integer.parseInt(yfield.getText());
//            width = Integer.parseInt(widthfield.getText());
//            height = Integer.parseInt(heightfield.getText());
            x = 50;
            y = 50;
            width = 200;
            height = 300;

            System.out.println("Drawing rectangle...");
            System.out.println("x:" + x + " y:" + y + " width:" + width + " height:" + height);

            Rectangle rectangle = createRectangle(x, y, width, height);
            rootChildren.add(rectangle);
        });


        primaryStage.setTitle("Graphical editor");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    @FXML
    public void draw_rectangle_click(ActionEvent actionEvent) {
        x = Integer.parseInt(xfield.getText());
        y = Integer.parseInt(yfield.getText());
        width = Integer.parseInt(widthfield.getText());
        height = Integer.parseInt(heightfield.getText());

        System.out.println("Drawing rectangle...");
        System.out.println("x:" + x + " y:" + y + " width:" + width + " height:" + height);

        Rectangle rectangle = createRectangle(x, y, width, height);
        rootChildren.add(rectangle);


    }

    private Rectangle createRectangle(int x, int y, int width, int height){
        Rectangle rectangle = new Rectangle(x,y,width,height);
        rectangle.setLayoutY(50);
        rectangle.setFill(Color.TRANSPARENT);
        rectangle.setStroke(Color.BLUE);
        return rectangle;
    }

    @FXML
    public void draw_ellipse_click(ActionEvent actionEvent) {
        actionEvent.consume();
        System.out.println("Drawing ellipse...");
    }

    public static void main(String[] args) {
        launch(args);
    }
}
