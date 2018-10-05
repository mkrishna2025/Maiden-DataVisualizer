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
import { FILES } from "../../data";
import Button from "@material-ui/core/Button";
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core/styles";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

const drawerWidth = 240;

const theme = createMuiTheme({
  palette: {
    type: "dark" // Switching the dark mode on is a single property value change.
  }
});

const styles = theme => ({
  root: {
    flexGrow: 1,
    zIndex: 1,
    overflow: "hidden",
    position: "relative",
    width: "100%",
    minHeight: theme.spacing.unit * 8
  },
  button: {
    margin: theme.spacing.unit * 1.5,
    width: theme.spacing.unit * 27,
    backgroundColor: "#757575"
  },
  appBar: {
    position: "absolute",
    marginLeft: drawerWidth,
    backgroundColor: "#455A64",
    width: `calc(100% - ${drawerWidth}px)`
  },
  toolbar: theme.mixins.toolbar,
  drawerPaper: {
    width: drawerWidth,
    backgroundColor: "#212121",
    color: "#FFEBEE",
    overflow: "hidden",
    minHeight: theme.spacing.unit * 40
  },
  paper: {
    padding: theme.spacing.unit * 2,
    textAlign: "center",
    color: "#FFEBEE"
  },
  content: {
    backgroundColor: "#616161",
    width: `calc(100% - ${drawerWidth * 1.2}px)`,
    marginLeft: drawerWidth,
    overflow: "hidden",
    padding: theme.spacing.unit * 3,
    minHeight: theme.spacing.unit * 64.3
  },
  palette: {
    type: "dark" // Switching the dark mode on is a single property value change.
  },
  section: {
    margin: 10,
    border: "2px solid grey",
    borderRadius: 15,
    height: 220,
    zIndex: 1,
    overflow: "hidden"
  }
});

class FirstPage extends React.Component {
  constructor(props){
    super(props);
    this.onButtonClick = this.onButtonClick.bind(this);
  }
  onButtonClick(){
    this.props.history.push('addtable');
  }
  render() {
    const { classes, theme } = this.props;
    
    const drawer = (
      <div>
        <List style={{ marginLeft: 10 }}>Recently Used Tables</List>
        <p className={classes.section} />
        <Divider />
        <List style={{ marginLeft: 10 }}>Folder Navigation</List>
        <p className={classes.section} />
        <Divider />
        <Button variant="contained" className={classes.button} onClick={this.onButtonClick}>
          Create New Table
        </Button>
      </div>
    );

    return (
      <MuiThemeProvider theme={theme}>
        <div className={classes.root}>
          <AppBar className={classes.appBar}>
            <Toolbar>
              <Typography variant="title" color="inherit">
                Important Tables to be Bookmarked
              </Typography>
            </Toolbar>
          </AppBar>

          <Drawer
            variant="permanent"
            classes={{
              paper: classes.drawerPaper
            }}
            style={{ overflow: "hidden" }}
          >
            {drawer}
          </Drawer>
        </div>
        <main className={classes.content}>
          <div className={classes.toolbar} />
          {/* <Grid
            container
            justify="flex-start"
            spacing={8}
            style={{ marginLeft: -20 }}
          >
            {FILES.map(files => (
              <Grid item xs={2} value={files.value}>
                <Paper className={classes.paper}>{files.display}</Paper>
              </Grid>
            ))}
          </Grid> */}
        </main>
      </MuiThemeProvider>
    );
  }
}

FirstPage.propTypes = {
  classes: PropTypes.object.isRequired,
  theme: PropTypes.object.isRequired
};

export default withStyles(styles, { withTheme: true })(FirstPage);
