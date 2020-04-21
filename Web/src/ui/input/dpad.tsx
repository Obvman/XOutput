import React from "react";
import Grid from '@material-ui/core/Grid';
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';
import StopIcon from '@material-ui/icons/Stop';
import { withStyles, Theme } from "@material-ui/core";
import { grey } from '@material-ui/core/colors';
import { Styles } from "@material-ui/core/styles/withStyles";

export interface InputDpadProps {
  up: number;
  down: number;
  left: number;
  right: number;
  classes?: any;
}

const styles: Styles<Theme, any, any> = (theme) => ({
  container: {
    width: '75px',
    margin: 'auto',
  },
  active: {
    backgroundColor: theme.palette.primary.main,
    borderRadius: '30px',
    color: grey[50],
  }
});

export class InputDpadComponent extends React.Component<InputDpadProps, any, any> {

  constructor(props: Readonly<any>) {
    super(props);
  }

  private getActiveIndex(): number {
    if (this.props.up) {
      if (this.props.left) {
        return 1;
      } else if (this.props.right) {
        return 3;
      } else {
        return 2;
      }
    } else if (this.props.down) {
      if (this.props.left) {
        return 7;
      } else if (this.props.right) {
        return 9;
      } else {
        return 8;
      }
    } else {
      if (this.props.left) {
        return 4;
      } else if (this.props.right) {
        return 6;
      } else {
        return 5;
      }
    }
  }

  private getColor(themeClass: string, i: number): string {
    return this.getActiveIndex() == i ? themeClass : '';
  }

  render() {
    const { classes } = this.props;

    return (<Grid container className={classes.container}>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 1)} style={{ rotate: '-45deg'}}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 2)}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 3)} style={{ rotate: '45deg'}}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 4)} style={{ rotate: '-90deg'}}/></Grid>
      <Grid item xs={4}><StopIcon className={this.getColor(classes.active, 5)}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 6)} style={{ rotate: '90deg'}}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 7)} style={{ rotate: '-135deg'}}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 8)} style={{ rotate: '180deg'}}/></Grid>
      <Grid item xs={4}><ArrowUpwardIcon className={this.getColor(classes.active, 9)} style={{ rotate: '135deg'}}/></Grid>
    </Grid>);
  }
}

export const DpadComponent = withStyles(styles)(InputDpadComponent);
