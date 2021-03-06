//package sample;
//
//import javafx.event.ActionEvent;
//import javafx.fxml.FXML;
//import javafx.fxml.FXMLLoader;
//import javafx.fxml.Initializable;
//import javafx.event.Event;
//import javafx.scene.Node;
//import javafx.collections.ObservableList;
//import javafx.scene.paint.Color;
//
//import java.io.IOException;
//import java.net.URL;
//import java.util.ResourceBundle;
//import javafx.scene.*;
//import javafx.application.Application;
//import javafx.scene.shape.Line;
//import javafx.stage.Stage;
//import javafx.event.*;
//import java.lang.*;
//import javafx.scene.control.Label;
//import javafx.scene.control.TextField;
//import javafx.scene.shape.Rectangle;
//
//import java.awt.*;
//
//
//public class GEController extends Application{
//
//    @FXML public TextField xfield;
//    @FXML public TextField yfield;
//    @FXML public TextField widthfield;
//    @FXML public TextField heightfield;
//
//    private int x, y, width, height;
//
//    @Override
//    public void start(Stage primaryStage) throws IOException {
//
//        primaryStage.setTitle("Graphical editor");
//        Rectangle rectangle = new Rectangle(50, 50, 200, 200);
//        Group group = new Group(rectangle);
//        Scene scene = new Scene(group, 1620, 970);
//        primaryStage.setScene(scene);
//        primaryStage.show();
//    }
//
//    @FXML
//    public void draw_rectangle_click(ActionEvent actionEvent) {
//        x = Integer.parseInt(xfield.getText());
//        y = Integer.parseInt(yfield.getText());
//        width = Integer.parseInt(widthfield.getText());
//        height = Integer.parseInt(heightfield.getText());
//
//        System.out.println("Drawing rectangle...");
//        System.out.println("x:" + x + " y:" + y + " width:" + width + " height:" + height);
//
//
////        Rectangle rectangle = new Rectangle(x,y,width,height);
////        Group root = new Group(rectangle);
////        Scene scene = new Scene(root, 600, 300);
////        primary
//    }
//
//    public void draw_ellipse_click(ActionEvent actionEvent) {
//        actionEvent.consume();
//        System.out.println("Drawing ellipse...");
//    }
//
//    public static void main(String args[]){
//        launch(args);
//    }
//
//}


