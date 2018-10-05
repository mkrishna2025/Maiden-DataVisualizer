import React from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import { FILES } from "./data";
import Button from "@material-ui/core/Button";
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core/styles";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import CreateTable from "./controllers/secondpage/index.js";
import FirstPage from "./controllers/firstpage/index.js";

class App extends React.Component {
  render() {
    return (
      <div>
        <Route path="/home" exact component={FirstPage} />
        <Route path="/addtable" component={CreateTable} />
      </div>
    );
  }
}

export default App;
