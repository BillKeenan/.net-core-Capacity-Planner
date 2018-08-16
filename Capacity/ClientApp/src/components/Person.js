import React, { Component } from 'react';

export class Person extends Component {
  displayName = Person.name

  constructor(props) {
    super(props);
    this.state = { input:{firstName: "",lastName:"",email:"" },blurred:false};
  }

  render() {
    return (
      <div>
        <PersonForm />
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <p>Current count: <strong>{this.state.currentCount}</strong></p>

      </div>
    );
  }
}

export class PersonForm extends Component {

  constructor(props) {
    super(props);

    this.state = {input:{firstName: "",lastName:"",email:"" },blurred:false};


    // var prj = props.project;

    //       fetch('api/Capacity/'+prj)
    //         .then(response => response.json())
    //         .then(data => {
    //           this.setState({ capacity: data, loading: false, });
    //       });
  }
  validate() {
    const errors = {};
    const {input} = this.state;
    
    if (!input.firstName || input.name.firstName<3) {
        errors.firstName = 'Name is required and must be at least 10 characters  ';
    } 
    return {
        errors,
        isValid: Object.keys(errors).length === 0
    };
  }

  render() {

    const {input, blurred} = this.state;
    const {errors, isValid} = this.validate();

    if (this.state.loading){
      return ( "loading" );
    }else{
      return(
        <form onSubmit={this.handleSubmit}>
        <div>
          <label> 
          First Name:
            <input 
              name="firstName" 
              ref={(input) => { this.firstName = input; }} 
              onBlur={() => this.handleBlur('firstName')}
              type="text" value={input.firstName}  
              onChange={e => this.handleInputChange({firstName: e.target.value})} 
            />
          </label>
          {blurred.firstName && !!errors.firstName && <span>{errors.firstName}</span>}
        </div>

        <div>
          <label> 
          Last Name:
            <input 
              name="lastName" 
              ref={(input) => { this.lastName = input; }} 
              onBlur={() => this.handleBlur('lastName')}
              type="text" value={input.lastName}  
              onChange={e => this.handleInputChange({lastName: e.target.value})} 
            />
          </label>
          {blurred.lastName && !!errors.lastName && <span>{errors.lastName}</span>}
        </div>
        
        <button type="submit" disabled={!isValid}>
                        Submit
                    </button>
      </form>
      )
    }
  }
}
