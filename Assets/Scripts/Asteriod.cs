using UnityEngine;
using System.Collections;


    public class Asteriod : MonoBehaviour
    {
        #region members
        private Vector3 m_target;
        private Transform m_transform;
        private float m_minSpeed;
        private float m_maxSpeed;
        private Vector3 m_startingPos;
        #endregion

        #region mono functions
        // Use this for initialization
        void Awake()
        {

            m_transform = this.gameObject.transform;
            m_startingPos = this.gameObject.transform.position;
            Debug.Log("m_transform.position" + m_transform.position);

    }
    public void Start()
        {
            m_target = new Vector3(0,0,2);
            StartCoroutine(MoveFoward());
            Debug.Log("Start");
        }
        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("collided with " + other.name);
            //if (other.name == "EarthContainer")
            //{
            //    Destroy();
            //}
            //else
            //{
            //    other.GetComponent<Asteriod>().Destroy();
            //}

        }
        #endregion

        #region public functions

        public void Destroy()
        {

            this.gameObject.SetActive(false);
            
        }

        public void StartMovement()
        {
            StartCoroutine(MoveFoward());
        }
        #endregion

        #region private functions

        private IEnumerator MoveFoward()
        {
            Vector3 targetMovePosition = m_target;
            float distance = 0;

            distance = Vector3.Distance(m_transform.position, targetMovePosition);
            float speed = Random.Range(0.5f, 2f);
            m_transform.LookAt(m_target);
            float distanceToStopAt = 0.1f;
            Vector3 moveto = m_transform.forward * 2f;
            while(true) {
                moveto = m_transform.position + (m_transform.forward * 2f);
                m_transform.position = Vector3.MoveTowards(m_transform.position, moveto, Time.deltaTime * speed);
                //Debug.Log(m_transform.position);
                //distance = Vector3.Distance(m_transform.position, targetMovePosition);
                yield return null;
             }
        //destroy();
        //yield break;
        }
        #endregion

        #region properties
        public Vector3 Target
        {
            get
            {
                return m_target;
            }
            set
            {
                m_target = value;
            }
        }

        public float MinSpeed
        {
            get
            {
                return m_minSpeed;
            }
            set
            {
                m_minSpeed = value;
            }
        }

        public float MaxSpeed
        {
            get
            {
                return m_maxSpeed;
            }
            set
            {
                m_maxSpeed = value;
            }
        }
        #endregion
    }

