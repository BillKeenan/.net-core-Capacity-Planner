import React, { Component } from 'react';

export class Project extends Component {
  displayName = Project.name

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.addProject = this.addProject.bind(this);
    
    if (String(this.props.location.pathname) === "/project/create"){

      this.state = {
        input: {
            firstName: "",
            lastName: ""
        },
        blurred: {
          firstName: false,
          lastName: false
        }
      };
    }else{
      
      this.state = {projects: [], loading: true, }

      fetch('api/Project')
        .then(response => response.json())
        .then(data => {
          this.setState({ projects: data, loading: false, });
        });

    }

  }



  incrementCounter() {
    fetch('api/Project', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstParam: 'yourValue',
        secondParam: 'yourOtherValue',
      })
    })
  }

  addProject(){

    var project = {
      name: 'Test Project'
    };

    (async () => {
      const rawResponse = await fetch('api/Project', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(project)
      });
      const content = await rawResponse.json();
      this.setState(prevState => ({
        projects: [...prevState.projects, project]
      }))
    })();
    
  }

  handleInputChange(newPartialInput) {
    this.setState(state => ({
        ...state,
        input: {
            ...state.input,
            ...newPartialInput
        }
    }))
}

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  
  handleSubmit(event) {
    alert('A project was submitted: ' + this.state.input.firstName + ':'+this.state.input.lastName);
    event.preventDefault();
    fetch('api/Project', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstName: this.state.input.name
      })
    })
  }


  render() {
    // like this https://reactjs.org/docs/thinking-in-react.html
    if (this.state.loading ){
      return (<div>loading</div>);
    }else{
      var projectHTML = this.state.projects.map(function(d, idx){
        return <tr>
        <td key={idx}>{d.name}</td> <ProjectCapacity project={d.name}/>
        </tr>
      },this);

      return (
      <div>
        <div>
          <table>
          {projectHTML}
        </table>
        </div>
          <div>
            <h1>Counter</h1>
            <p>This is a simple example of a React component.</p>
            <p>Current count: <strong>{this.state.currentCount}</strong></p>
            <button onClick={this.addProject}>Increment</button>
          </div>
        </div>
      );
    }
  }

  componentDidMount(){
    if (String(this.props.location.pathname) === "/project/create"){

    this.Name.focus();
    }
  }
  handleBlur(fieldName) {
    this.setState(state => ({
        ...state,
        blurred: {
            ...state.blurred,
            [fieldName]: true
        }
    }))
  }
  validate() {
    const errors = {};
    const {input} = this.state;
    
    if (!input.name || input.name.length<10) {
        errors.name = 'Name is required and must be at least 10 characters  ';
    } 
    return {
        errors,
        isValid: Object.keys(errors).length === 0
    };
}

  renderForm() {
    const {input, blurred} = this.state;
    const {errors, isValid} = this.validate();
    
    return (
      <form onSubmit={this.handleSubmit}>
        <div>
          <label>
          Name:
            <input 
              name="Name" 
              ref={(input) => { this.name = input; }} 
              onBlur={() => this.handleBlur('name')}
              type="text" value={input.name} 
              onChange={e => this.handleInputChange({name: e.target.value})} 
            />
          </label>
          {blurred.name && !!errors.name && <span>{errors.name}</span>}
        </div>
        
        <button type="submit" disabled={!isValid}>
                        Submit
                    </button>
      </form>
    );

    
  }

}


export class ProjectCapacity extends Component {

  constructor(props) {
    super(props);

    this.state = { capacity: [], loading: true };

    var prj = props.project;

          fetch('api/Capacity/'+prj)
            .then(response => response.json())
            .then(data => {
              this.setState({ capacity: data, loading: false, });
          });
  }

  render() {

    if (this.state.loading){
      return ( "loading" );
    }
      return(
        this.state.capacity.map(function(d, idx){
          return <td>{d}</td>
        },this)
        );
    }
  }