import React, { Component } from '../../../../../../Library/Caches/typescript/2.9/node_modules/@types/react';
import { Col, Grid, Row } from 'react-bootstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={3}>
            <NavMenu />
          </Col>
          <Col sm={9}>
            {this.props.children}
          </Col>
        </Row>
      </Grid>
    );
  }
}
